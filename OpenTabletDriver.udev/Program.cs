using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;
using libudev.Rules;
using libudev.Rules.Names;
using Newtonsoft.Json;
using TabletDriverPlugin.Tablet;

namespace OpenTabletDriver.udev
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new RootCommand("OpenTabletDriver udev rule tool")
            {
                new Argument<DirectoryInfo>("directory"),
                new Argument<FileInfo>("output"),
                new Option(new string[] { "--verbose", "-v" }, "Verbose output")
                {
                    Required = false,
                    Argument = new Argument<bool>("verbose")
                },
                new Option(new string[] { "--libinput-override", "-l" }, "Apply the libinput override")
                {
                    Required = false,
                    Argument = new Argument<bool>("libinputOverride")
                }
            };

            root.Handler = CommandHandler.Create<DirectoryInfo, FileInfo, bool, bool>(WriteRules);
            root.Invoke(args);
        }

        static async Task WriteRules(DirectoryInfo directory, FileInfo output, bool verbose = false, bool libinputOverride = false)
        {
            if (output.Exists)
                await Task.Run(output.Delete);
            if (!output.Directory.Exists)
                output.Directory.Create();
            var path = output.FullName.Replace(Directory.GetCurrentDirectory(), string.Empty);
            Console.WriteLine($"Writing all rules to '{path}'...");
            using (var sw = output.AppendText())
            {
                await foreach (var rule in CreateRules(directory, libinputOverride))
                {
                    await sw.WriteLineAsync(rule);
                    if (verbose)
                        Console.WriteLine(rule);
                }
            }
            Console.WriteLine("Finished writing all rules.");
        }

        static async IAsyncEnumerable<string> CreateRules(DirectoryInfo directory, bool libinputOverride)
        {
            await foreach (var tablet in GetAllConfigurations(directory))
            {
                if (string.IsNullOrWhiteSpace(tablet.TabletName))
                    continue;
                yield return string.Format("# {0}", tablet.TabletName);
                yield return RuleCreator.CreateAccessRule(tablet, "0666");
                if (libinputOverride)
                    yield return RuleCreator.CreateOverrideRule(tablet);
            }
        }

        static async IAsyncEnumerable<TabletProperties> GetAllConfigurations(DirectoryInfo directory)
        {
            var files = await Task<IEnumerable<string>>.Run(() => Directory.GetFiles(directory.FullName, "*.json", SearchOption.AllDirectories));
            foreach (var path in files)
            {
                var file = new FileInfo(path);
                yield return await Task.Run<TabletProperties>(() => TabletProperties.Read(file));
            }
        }
    }
}
