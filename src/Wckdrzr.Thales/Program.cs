using System;

namespace Wckdrzr.AutomaticVersionUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Auto Build Number Package Running...");

            Executor e = new Executor();
            e.Execute();
            Console.WriteLine("Version Update Complete");

        }
    }
}

