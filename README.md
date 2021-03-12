# TLCS: Top level C# made easier
TLCS is a command line utility, which makes using top level csharp a whole lot easier.

## Life before TLCS
Before using TLCS, you would need to type,
```bat
md TestProgramRepo
cd TestProgramRepo
dotnet new console
code Program.cs
dotnet run
```
And then you have another bloated repository on your system, even though all you need is top level C#.

## Life after TLCS
Now all you have to type is,
```bat
echo. > Program.cs
code Program.cs
tlcs Program.cs
```
and everything is handled for you! No repo; no nonsense.

# Features of TLCS
- Type `tlcs -t` to directly write C# into the terminal
- Type `tlcs -s` to write a single-liner as an argument
- Type `tlcs -h` or `tlcs help` to open the help menu
- Type `tlcs {FILE_NAME}` to run a `.cs` file
- Type `tlcs {FILE_NAME} {args}` if your C# file uses `string[] args`
