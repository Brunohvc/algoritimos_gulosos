using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BruteForce
{
    public class Program
    {
        public static char[] letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
        public static int[] contLetra = new int[6];
        public static bool auth = false;
        public static bool finished = false;
        public static string senhaFinal = "";
        public static int quantidadeLetras = 0;
        public static int quantidadeContLetras = 0;
        public static int tentativas = 0;
        static void Main(string[] args)
        {
            contLetra[0] = 0;
            contLetra[1] = 0;
            contLetra[2] = 0;
            contLetra[3] = 0;
            contLetra[4] = 0;
            contLetra[5] = 0;
            quantidadeLetras = letras.Length;
            quantidadeContLetras = contLetra.Length;

            int cont = 0;

            Console.WriteLine("Inicio: "+ DateTime.Now);
            while (!auth && !finished)
            {
                if (cont < 100)
                {
                    cont++;
                    Thread t = new Thread(NovaThread);
                    t.Priority = ThreadPriority.Highest;
                    t.Start();
                }
            }
            Console.WriteLine("Fim: " + DateTime.Now);

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


        static void NovaThread()
        {
            while (!auth && !finished)
            {
                try
                {
                    var senhaTentativa = String.Format("{0}{1}{2}{3}{4}{5}",
                    letras[contLetra[5]],
                    letras[contLetra[4]],
                    letras[contLetra[3]],
                    letras[contLetra[2]],
                    letras[contLetra[1]],
                    letras[contLetra[0]]);
                    var strArguments = String.Format("{0} {1}", "administrador", senhaTentativa);
                    tentativas++;
                    if(tentativas % 5000 == 0)
                    {
                        Console.WriteLine(DateTime.Now + " - Tentativa: " + tentativas + " ==> " + senhaTentativa);
                    }

                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = @"C:\Users\bruno\Projetos\GitHub\algoritimos_gulosos\Auth\Auth\bin\Debug\netcoreapp3.1\Auth.exe";
                        process.StartInfo.Arguments = strArguments;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.Start();

                        StreamReader reader = process.StandardOutput;
                        string output = reader.ReadToEnd();

                        process.WaitForExit();

                        // Console.WriteLine("Tentativa: " + senhaTentativa + " --> Saída: " + output);

                        var validacao = output.Replace("\r\n", "").Equals("Login ou senha incorretos!");

                        if (!validacao)
                        {
                            Console.WriteLine("ACHOU!" + validacao);
                            auth = true;
                            senhaFinal = senhaTentativa;
                        }
                        else
                        {
                            for (var op = 0; op < quantidadeContLetras; op++)
                            {
                                if (op == 0)
                                {
                                    contLetra[op]++;
                                }

                                if (contLetra[op] + 1 > quantidadeLetras)
                                {
                                    if (op != 5)
                                    {
                                        contLetra[op + 1]++;
                                        contLetra[op] = 0;
                                    }
                                } else if (op == quantidadeContLetras)
                                {
                                    if (contLetra[4] == quantidadeLetras && contLetra[5] == quantidadeLetras)
                                    {
                                        Console.WriteLine("FINISHED");
                                        finished = true;
                                    }
                                }
                                else 
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    var tentativa = String.Format("Erro no cont letra: {0} - {1} - {2} - {3} - {4} - {5}",
                    contLetra[5],
                    contLetra[4],
                    contLetra[3],
                    contLetra[2],
                    contLetra[1],
                    contLetra[0]);

                    Console.WriteLine("Hora do Erro: " + DateTime.Now +" ==> " + tentativa);
                }
            }
        }
    }
}
