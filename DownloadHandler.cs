using System;
using System.Text.Json;

namespace GangstaXSCPBot
{
    public class DownloadHandler
    {
        public static string[] GetServerInfo(string APILink)
        {

            // Use WebClient to download the string and put in var
            var serverInfoJSON = string.Empty;
            using (var webClient = new System.Net.WebClient())
            {
            // Try to download, with support for running it too many times
            Sus:
                try
                {
                    serverInfoJSON = webClient.DownloadString(APILink);
                }
                catch (System.Net.WebException ex)
                {
                    // inform user
                    Console.WriteLine("** You've requested info too many times without waiting");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Waiting five seconds");
                    System.Threading.Thread.Sleep(5000);
                    goto Sus;
                }
            }

            // parse success - this will pretty much always be yes, unless the api dies
            using (JsonDocument document = JsonDocument.Parse(serverInfoJSON))
            {
                JsonElement root = document.RootElement;
                JsonElement successElement = root.GetProperty("Success");

                // was success? if not, inform user
                bool success = successElement.GetBoolean();
                if (!success)
                {
                    Console.WriteLine("** API has not returned a success, please check");
                    Console.WriteLine(serverInfoJSON);
                }
            }

            string serverPlayersTotal = String.Empty;
            int numServers = 0;
            using (JsonDocument document = JsonDocument.Parse(serverInfoJSON))
            {
                JsonElement root = document.RootElement;
                // get server array
                JsonElement serverElement = root.GetProperty("Servers");

                // Get server count
                numServers = serverElement.GetArrayLength();

                // go through each server's player value and add it to string
                foreach (JsonElement server in serverElement.EnumerateArray())
                {
                    // find players value, if not set it to empty
                    if (server.TryGetProperty("Players", out JsonElement playersElement))
                    {
                        serverPlayersTotal += playersElement.GetString() + " ";
                    }
                    else
                    {
                        serverPlayersTotal = String.Empty;
                    }
                }
            }

            // get the executor to split
            string[] infoToReturn;
            infoToReturn = new string[numServers + 1];
            infoToReturn[0] = serverPlayersTotal.ToString();
            infoToReturn[1] = numServers.ToString();

            // array index 0 is player list of servers, in order
            // array index 1 is amount of server it found
            return infoToReturn;

        }
    }
}
