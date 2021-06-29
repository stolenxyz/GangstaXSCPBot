using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GangstaXSCPBot
{
    public class StatusHandler
    {
        public static Task ActivityEvery20SecondsAsync(DiscordClient client)
        {
            // Run forever
            while(true)
            {
                // Time date format (not yucky american)
                var culture = new CultureInfo("en-GB");
                // Stuff to get player count (as usual)
                var json = string.Empty;
                using (var fs = File.OpenRead("config.json"))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = sr.ReadToEnd();
                var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
                string[] servInfo = DownloadHandler.GetServerInfo(configJson.APILink);
                string[] concatServerPlayers = servInfo[0].Split(' ');
                string server1Players = concatServerPlayers[0];
                string server2Players = concatServerPlayers[1];

                // The string that we want to set to activity (final string)
                string toActivity = $"1: {server1Players} | 2: {server2Players}";

                // Inform console what we're setting it to
                Console.Write($"Setting activity to: {toActivity} at {DateTime.Now.ToString(culture)}");

                // Send player count to relevant server file
                using StreamWriter fileSRV1 = new($"PlayersServer1.txt", append: true);
                fileSRV1.WriteLineAsync($"{server1Players} {DateTime.Now.ToString(culture)}");

                using StreamWriter fileSRV2 = new($"PlayersServer2.txt", append: true);
                fileSRV2.WriteLineAsync($"{server2Players} {DateTime.Now.ToString(culture)}");

                // and finally, set it

                DiscordActivity activity = new DiscordActivity();
                DiscordClient discord = client;
                activity.Name = toActivity;
                activity.ActivityType = ActivityType.Playing;
                discord.UpdateStatusAsync(activity);

                Console.WriteLine(" - waiting...");
                Thread.Sleep(15000);
            }
        }
    }
}
