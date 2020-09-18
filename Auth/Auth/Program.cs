using System;

namespace Auth
{
    class Program
    {
        static void Main(string[] args)
        {
            var loginD = "administrador";
            var passwordD = "GGSENH";

            if (args.Length != 2)
            {
                Console.WriteLine("Digite 2 parâmetros!");
            }
            else
            {
                if (args[0].Equals(loginD) && args[1].Equals(passwordD))
                {
                    Console.WriteLine("Hacked!");
                }
                else
                {
                    Console.WriteLine("Login ou senha incorretos!");
                }
            }
        }
    }
}
