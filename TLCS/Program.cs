using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TLCS
{
    class Program
    {
        const string DOTNETPATH = "/dotnetrepo";
        const string NAMEOFPROJECT = "/dotnetrepo.csproj";
        enum CmdType{ file, single, terminal }
        static void Main(string[] args)
        {
            var type = CmdType.file;
             if (args.Length == 0) { Console.WriteLine("Error Code 1: No File Path Specified"); return; }
             if (!File.Exists(args[0]))
             {
                if(args[0] == "help" || args[0] == "-h")
                {
                    Console.WriteLine("- Type in a csharp file to run it (e.g. tlcs Program.cs)");
                    Console.WriteLine("- Type in help to see this menu.");
                    Console.WriteLine($"- Type -s as the first argument, and then your command in the following argument. (e.g. tlcs -s {'"'}System.Console.WriteLine(200);{'"'} )");
                    Console.WriteLine("- Type -t to make a csharp terminal, to type in (tlcs -t)");
                    return;
                }
                else if (args[0] == "-s") { type = CmdType.single; goto IgnoreLackOfFile; }
                else if (args[0] == "-t") { type = CmdType.terminal; goto IgnoreLackOfFile; }
                Console.WriteLine("Error Code 2: Invalid File Path");
                return;
             }
             
             if(Path.GetExtension(args[0]) != ".cs") { Console.WriteLine("Error Code 3: The File Specified Is Not Of Type .cs"); return; }
        IgnoreLackOfFile:
            #region TLCSMainCode
            string file = string.Empty;
            switch (type)
            {
                case CmdType.file:
                    file = File.ReadAllText(args[0]);
                    break;
                case CmdType.single:
                    file = args[1];
                    break;
                case CmdType.terminal:
                    Console.WriteLine();
                    while (true)
                    {
                        file = Console.ReadLine();
                        string nstr = Path.GetTempPath();
                        Run(nstr, file);
                        Console.WriteLine();
                    }
                    
            }
            
            string strWorkPath = Path.GetTempPath();

            Run(strWorkPath, file);
            #endregion
        }
        static void Run(string strWorkPath, string file)
        {
            if (!Directory.Exists(strWorkPath + DOTNETPATH)) { Directory.CreateDirectory(strWorkPath + DOTNETPATH); }
            if (!File.Exists(strWorkPath + DOTNETPATH + NAMEOFPROJECT))
            {
                Console.WriteLine("Creating new dotnet instance...");
                using (var p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c cd {'"'}{strWorkPath + DOTNETPATH}{'"'} && dotnet new console --force",
                        RedirectStandardOutput = true

                    };
                    p.Start();
                    p.WaitForExit();
                }

            }
            using (FileStream fileStream = new FileStream(strWorkPath + DOTNETPATH + "/Program.cs", FileMode.Create))
            {
                fileStream.Write(Encoding.UTF8.GetBytes(file));
            }
            Console.WriteLine();
            using (var p = new Process())
            {
                p.StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c cd {'"'}{strWorkPath + DOTNETPATH}{'"'} && dotnet run"
                };
                p.Start();
                p.WaitForExit();
            }
        }
    }
}
