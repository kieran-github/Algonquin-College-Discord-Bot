using System.Threading.Tasks;
using Discord.Commands;
using System.IO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Discord;

namespace algonquinCollegeDiscordBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        public String[] lines = File.ReadAllLines(@"D:\Computer Science - Programming libary\C# .NET\algonquinCollegeDiscordBot\algonquinCollegeDiscordBot\AcademicCalendarMar2018.txt");

        //pastebin of commands https://pastebin.com/w7x8Zq7v
        [Command("rules"), Summary("Displays the rules of the server")]
        public async Task Rules()
        {
            await Context.Channel.SendMessageAsync(@"
            Discord Rules:
            
            1. -  No Voice/Text/Reaction spam.
            2. -  No Racism/Gore/NSFW.
            3. -  Do not share personal information.
            4. -  No extreme harassment/flaming of users within the discord.
            5. -  Do not chat excessively in other languages.
            6. -  No advertising. (Unless you are a regular of the discord)
            
            All moderators can punish at their own discretion. A warning will be issued if continuous it will lead to a mute. In extreme cases, you may be banned. 
            These rules are tentative and punishments are resolved on a case-by-case basis.
            ");
        }
        [Command("commands"), Summary("Displays the current commands available to the discord user by enumerating the Modules directory.")]
        public async Task commands()
        {
            await Context.Channel.SendMessageAsync(
                "Current Supported Commands: \n" +
                "https://pastebin.com/w7x8Zq7v \n" +
                "\n" +
                "!commands\n" +
                "!invite\n" +
                "!rules\n" +
                "!sourcecode\n" +
                "!today\n" +
                "!week\n" +
                "!month\n" +
                "!map\n" +
                "!crypto\n" +
                "!crypto + currency name");

        }
        [Command("invite"), Summary("Displays the permanent invite link for the Algonquin College discord.")]
        public async Task Invite()
        {
            await Context.Channel.SendMessageAsync("https://discord.gg/VwukBWK");
        }

        [Command("sourcecode"), Summary("Displays the github repo for this discord bot.")]
        public async Task Sourcecode()
        {
            await Context.Channel.SendMessageAsync("https://github.com/kieran-github/Algonquin-College-Discord-Bot using discord.net api: https://discord.foxbot.me/docs/");
        }

        [Command("today"), Summary("Gets current date in form MMMM dd and then parses the academic calendar to see if there is a event.")]
        public async Task Today()
        {
            List<String> result = new List<String>();
            DateTime dateTime = DateTime.UtcNow.Date;
            foreach (string line in lines)
            {
                if (line.StartsWith(dateTime.ToString("MMMM dd")))
                {
                    result.Add(line);
                }
            }
            if (result.Count != 0)
            {
                String buffer = "";
                foreach (String cDate in result)
                {
                    buffer += cDate + "\n";
                }
                await Context.Channel.SendMessageAsync(buffer);
            }
            else
            {
                await Context.Channel.SendMessageAsync("Nothing special today.");
            }
        }
        [Command("week"), Summary("Gets current date in form MMMM dd and then parses the academic calendar for the week and sends it to the discords channel.")]
        public async Task Week()
        {
            int count = 0;
            DateTime dateTime = DateTime.UtcNow.Date;
            List<String> result = new List<String>();
            foreach (string line in lines)
            {
                if (line.StartsWith(dateTime.ToString("MMMM dd")))
                {
                    try
                    {
                        result.Add(lines[count]);
                        result.Add(lines[count + 1]);
                        result.Add(lines[count + 2]);
                        result.Add(lines[count + 3]);
                        result.Add(lines[count + 4]);
                        result.Add(lines[count + 5]);
                        result.Add(lines[count + 6]);
                    }
                    catch (EndOfStreamException)
                    { 
                        throw;
                    }
                }
                count++;
            }
            if (result.Count != 0)
            {
                String buffer = "";
                foreach (String cDate in result)
                {
                    buffer += cDate + "\n";
                }
                await Context.Channel.SendMessageAsync(buffer);
            }
            else
            {
                await Context.Channel.SendMessageAsync("Nothing special for the next seven days.");
            }
        }


        [Command("month"), Summary("Gets current date in form MMMM and then parses the academic calendar for the month and sends it to the discords channel.")]
        public async Task Month()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            List<String> result = new List<String>();
            foreach (string line in lines)
            {
                if (line.StartsWith(dateTime.ToString("MMMM")))
                {
                    result.Add(line);
                }
            }
            if (result.Count != 0)
            {
                String buffer = "";
                foreach (String cDate in result)
                {
                    buffer += cDate + "\n";
                }
                await Context.Channel.SendMessageAsync(buffer);
            }
            else
            {
                await Context.Channel.SendMessageAsync("Nothing special for this month.");
            }
        }
        



        [Command("map"), Summary("Pulls the Algonquin college map as pdf and displays it aswell as download link.")]
        public async Task Map()
        {
            await Context.Channel.SendMessageAsync("http://www.algonquincollege.com/parking/files/2017/05/Parking-Map_w-legend_8.5x11_May2017.pdf");
            await Context.Channel.SendFileAsync("D:\\Computer Science - Programming libary\\C# .NET\\algonquinCollegeDiscordBot\\algonquinCollegeDiscordBot\\Parking-Map_w-legend_8.5x11_May2017.pdf");
        }
    }

    [Group("crypto"), Summary("A class that will handle the multiple commands for crypto pricing.")]
    public partial class Crytpo : ModuleBase<SocketCommandContext>
    {
        [Command, Summary("Using the coinbase API, makes a REST API request for current price of bitcoin.")]
        public async Task Bitcoin()
        {
            using (var httpClient = new HttpClient())
            {
                EmbedBuilder builder = new EmbedBuilder();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var responseBTC = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/btc-usd/spot")).Result;
                var responseETH = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/eth-usd/spot")).Result;
                var responseLTC = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/ltc-usd/spot")).Result;
                dynamic dataBTC = JObject.Parse(responseBTC);
                dynamic dataETH = JObject.Parse(responseETH);
                dynamic dataLTC = JObject.Parse(responseLTC);
                builder.WithTitle("Current Crypto prices: ")
                    .AddField("BTC:", $"{dataBTC.data.amount}")
                    .AddField("ETH:", $"{dataETH.data.amount}")
                    .AddField("LTC:", $"{dataLTC.data.amount}")

                    .WithColor(Color.Blue);

                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("bitcoin")]
        public async Task BitcoinOnly()
        {
            using (var httpClient = new HttpClient())
            {
                EmbedBuilder builder = new EmbedBuilder();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var responseBTC = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/btc-usd/spot")).Result;
                dynamic dataBTC = JObject.Parse(responseBTC);
                builder.WithTitle("Current Crypto prices: ")
                    .AddField("BTC:", $"{dataBTC.data.amount}")
                    .WithColor(Color.Blue);

                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("btc")]
        public async Task BitcoinShort()
        {
            using (var httpClient = new HttpClient())
            {
                EmbedBuilder builder = new EmbedBuilder();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var responseBTC = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/btc-usd/spot")).Result;
                dynamic dataBTC = JObject.Parse(responseBTC);
                builder.WithTitle("Current Crypto prices: ")
                    .AddField("BTC:", $"{dataBTC.data.amount}")
                    .WithColor(Color.Blue);

                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("ethereum")]
        public async Task EthereumOnly()
        {
            using (var httpClient = new HttpClient())
            {
                EmbedBuilder builder = new EmbedBuilder();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var responseBTC = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/eth-usd/spot")).Result;
                dynamic dataETH = JObject.Parse(responseBTC);
                builder.WithTitle("Current Crypto prices: ")
                    .AddField("ETH:", $"{dataETH.data.amount}")
                    .WithColor(Color.Blue);

                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("eth")]
        public async Task EthereumShort()
        {
            using (var httpClient = new HttpClient())
            {
                EmbedBuilder builder = new EmbedBuilder();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var responseBTC = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/eth-usd/spot")).Result;
                dynamic dataETH = JObject.Parse(responseBTC);
                builder.WithTitle("Current Crypto prices: ")
                    .AddField("ETH:", $"{dataETH.data.amount}")
                    .WithColor(Color.Blue);

                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("litecoin")]
        public async Task LitecoinOnly()
        {
            using (var httpClient = new HttpClient())
            {
                EmbedBuilder builder = new EmbedBuilder();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var responseLTC = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/ltc-usd/spot")).Result;
                dynamic dataLTC = JObject.Parse(responseLTC);
                builder.WithTitle("Current Crypto prices: ")
                    .AddField("LTC:", $"{dataLTC.data.amount}")
                    .WithColor(Color.Blue);

                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("ltc")]
        public async Task LitecoinShort()
        {
            using (var httpClient = new HttpClient())
            {
                EmbedBuilder builder = new EmbedBuilder();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var responseLTC = httpClient.GetStringAsync(new Uri("https://api.coinbase.com/v2/prices/ltc-usd/spot")).Result;
                dynamic dataLTC = JObject.Parse(responseLTC);
                builder.WithTitle("Current Crypto prices: ")
                    .AddField("LTC:", $"{dataLTC.data.amount}")
                    .WithColor(Color.Blue);

                await ReplyAsync("", false, builder.Build());

            }
        }
    }
}
