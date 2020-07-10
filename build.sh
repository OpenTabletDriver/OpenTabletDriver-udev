configurations=$PWD/../OpenTabletDriver/TabletDriverLib/Configurations
rules=$PWD/build/30-opentabletdriver.rules

dotnet run -p $PWD/OpenTabletDriver.udev/*.csproj -- -v "${configurations}" "${rules}"