using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGarten2;

namespace Trabalho_PI
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new HttpHost("http://localhost:8080");
            host.Add(DefaultMethodBasedCommandFactory.GetCommandsFor(typeof(TrelloController)));
            host.Open();
            Console.WriteLine("Trello server is running, press any key to continue...");
            Console.ReadKey();
            host.Close();
        }
    }
}
