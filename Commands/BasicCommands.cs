using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;

namespace GangstaXSCPBot.Commands
{
    class BasicCommands : BaseCommandModule
    {
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

        [Command("cum")]
        [Description("cum")]
        public async Task Cum(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("*cums*").ConfigureAwait(false);
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
