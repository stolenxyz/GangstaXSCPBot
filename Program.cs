using System;

namespace GangstaXSCPBot
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }

        /* 
         * To get a runnable executable for Linux, run this commadn in project root directory and go to bin/release/net blah blah/publish
         * dotnet publish -c release -r ubuntu.21.04-x64 --self-contained
         */


    }
}
