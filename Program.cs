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
         * To get a runnable executable for Linux, run this command in project root directory and go to bin\Release\net5.0\ubuntu.21.04-x64\publish
         * dotnet publish -c release -r ubuntu.21.04-x64 --self-contained
         */


    }
}
