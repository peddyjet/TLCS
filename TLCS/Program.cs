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
        static void Main(string[] args)
        {
            
             if (args.Length == 0) { Console.WriteLine("Error Code 1: No File Path Specified"); return; }
             if (!File.Exists(args[0]))
             {
                Console.WriteLine("Error Code 2: Invalid File Path");
                return;
             }
             if(Path.GetExtension(args[0]) != ".cs") { Console.WriteLine("Error Code 3: The File Specified Is Not Of Type .cs"); return; }
            string file = File.ReadAllText(args[0]);
            string strWorkPath = Path.GetTempPath();
            if(!Directory.Exists(strWorkPath + DOTNETPATH)) { Directory.CreateDirectory(strWorkPath + DOTNETPATH); }
            if(!File.Exists(strWorkPath + DOTNETPATH + NAMEOFPROJECT)) 
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
            using (FileStream fileStream = new FileStream(strWorkPath + DOTNETPATH + "/Program.cs",FileMode.Create))
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
