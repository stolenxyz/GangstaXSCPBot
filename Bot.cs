using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using GangstaXSCPBot.Commands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GangstaXSCPBot
{
    class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public static string version = "1.0.1";
        public async Task RunAsync()
        {
            // Open config.json and deserialise so we can read the properties
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug   
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] {configJson.Prefix},
                EnableMentionPrefix = false,
                EnableDms = false,
                DmHelp = false,
                UseDefaultCommandHandler = true,
                IgnoreExtraArguments = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<BasicCommands>();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }
        private Task OnClientReady(DiscordClient client, ReadyEventArgs e)
        {
            Console.WriteLine("Client ready");
            StatusHandler.ActivityEvery20SecondsAsync(client);
            return Task.CompletedTask;
        }
    }
}
