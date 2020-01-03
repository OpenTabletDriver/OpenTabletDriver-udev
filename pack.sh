dotnet pack
dotnet tool uninstall -g OpenTabletDriver.udev
dotnet tool install -g --add-source ./OpenTabletDriver.udev/nupkg OpenTabletDriver.udev