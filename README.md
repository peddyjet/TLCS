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

# Roadmap
Currently we are on **version 1.0**, which includes:
- Basic top level programming
- No argument support

However, **version 1.1** is being planned, which will include:
- Argument support
- Single liner support
- Help menus

Down the road, we would like to include:
- direct coding into the terminal