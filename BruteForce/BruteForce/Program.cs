using System;
using System.Diagnostics;
using System.IO;

namespace BruteForce
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
            int[] contLetra = new int [6];
            contLetra[0] = 0;
            contLetra[1] = 0;
            contLetra[2] = 0;
            contLetra[3] = 0;
            contLetra[4] = 0;
            contLetra[5] = 0;
            var auth = false;
            var finished = false;
            var senhaFinal = "";

            while (!auth && !finished)
            {
                var senhaTentativa = String.Format("{0}{1}{2}{3}{4}{5}", 
                    letras[contLetra[5]], 
                    letras[contLetra[4]], 
                    letras[contLetra[3]], 
                    letras[contLetra[2]], 
                    letras[contLetra[1]], 
                    letras[contLetra[0]]);
                var strArguments = String.Format("{0} {1}", "administrador", senhaTentativa);

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = @"C:\Users\bruno\Desktop\auth equipe 2\Auth.exe";
                    process.StartInfo.Arguments = strArguments;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();

                    StreamReader reader = process.StandardOutput;
                    string output = reader.ReadToEnd();

                    process.WaitForExit();

                    Console.WriteLine("Tentativa: " + senhaTentativa + " --> Saída: " + output);

                    var validacao = output.Replace("\r\n","").Equals("Login ou senha incorretos!");

                    if (!validacao)
                    {
                        Console.WriteLine("ACHOU!" + validacao);
                        auth = true;
                        senhaFinal = senhaTentativa;
                    }
                    else
                    {
                        for(var op = 0; op < contLetra.Length; op++)
                        {
                            if(op == 0)
                            {
                                contLetra[op]++;
                            }

                            if(contLetra[op]+1 > letras.Length)
                            {
                                if(op != 5)
                                {
                                    contLetra[op + 1]++;
                                    contLetra[op] = 0;
                                }
                            }

                            if(contLetra[4] == letras.Length && contLetra[5] == letras.Length)
                            {
                                Console.WriteLine("FINISHED");
                                finished = true;
                            }
                        }
                    }

                }
            }

            if (auth)
            {
                Console.WriteLine("Senha: " + senhaFinal);
            }
            else
            {
                Console.WriteLine("Senha não encontrada!");
            }

            Console.ReadLine();
        }
    }
}
