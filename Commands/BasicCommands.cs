using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace GangstaXSCPBot.Commands
{
    class BasicCommands : BaseCommandModule
    {
        [Command("specplayers")]
        [Description("Returns the amount of players on the server you specify")]
        public async Task SCPOPlayer(CommandContext ctx, [Description("Server number to check")] int whichServer)
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            string[] servInfo = DownloadHandler.GetServerInfo(configJson.APILink);
            string[] serverPlayers = servInfo[0].Split(' ');


            int serverCalc = whichServer;
            // because arrays start at zero
            serverCalc--;
            string playersOnServer = serverPlayers[serverCalc];

            // Write player count to file
            Console.WriteLine("Writing player count to file...");
            using StreamWriter file = new($"PlayersServer{whichServer}.txt", append: true);
            await file.WriteLineAsync($"{playersOnServer}");

            await ctx.Channel.SendMessageAsync("This command will be removed in a future update to the bot, so please consider using the players command").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync($"Players on Server {whichServer}: {playersOnServer}").ConfigureAwait(false);
        }

        [Command("players")]
        [Description("Returns the amount of players on all servers")]
        public async Task SCPNPlayer(CommandContext ctx)
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            string[] servInfo = DownloadHandler.GetServerInfo(configJson.APILink);
            string[] concatServerPlayers = servInfo[0].Split(' ');

            string server1Players = concatServerPlayers[0];
            string server2Players = concatServerPlayers[1];

            // Time and Date things
            var culture = new CultureInfo("en-GB");

            // Send player count to relevant server file
            using StreamWriter fileSRV1 = new($"PlayersServer1.txt", append: true);
            await fileSRV1.WriteLineAsync($"{server1Players} {DateTime.Now.ToString(culture)}");

            using StreamWriter fileSRV2 = new($"PlayersServer2.txt", append: true);
            await fileSRV2.WriteLineAsync($"{server2Players} {DateTime.Now.ToString(culture)}");

            // Send message to Discord
            await ctx.Channel.SendMessageAsync($"Players on Server 1: {server1Players}").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync($"Players on Server 2: {server2Players}").ConfigureAwait(false);
        }

        [Command("datejoined")]
        [Description("Returns the date you joined this server")]
        public async Task DateJoined(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync(ctx.Member.JoinedAt.ToString()).ConfigureAwait(false);

        }

        [Command("rolecolour")]
        [Description("Returns your top-most role's colour in hex format")]
        public async Task Roles(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync(ctx.Member.Color.ToString()).ConfigureAwait(false);

        }

        [Command("info")]
        [Description("Returns information regarding the bot")]
        public async Task BotInfo(CommandContext ctx)
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var infoEmbed = new DiscordEmbedBuilder
            {
                Title = $"{configJson.BotTitle} Information",
                Description = "Information regarding the bot",
            };

            infoEmbed.AddField("Author", "Jatc251");
            infoEmbed.AddField("GitHub", "https://github.com/GangstaXwastaken/GangstaXSCPBot");
            infoEmbed.AddField("Discord", "discord.gg/tsv8xKBruP");
            infoEmbed.AddField("Version", Bot.version.ToString());

            await ctx.Channel.SendMessageAsync(embed: infoEmbed).ConfigureAwait(false);

        }
    }
}
