using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.IO;
using System;

namespace algonquinCollegeDiscordBot
{
    public class EntryPoint
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public static void Main(string[] args) => new EntryPoint().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });
            _handler = new CommandHandler(_client);
            await _client.LoginAsync(TokenType.Bot, creds.token);
            _client.Log += _client_Log;

            _client.UserJoined += AnnounceUserJoined;
            await _client.StartAsync();
            await Task.Delay(-1);
            
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync("Welcome to the algonquin college Discord server, please use !rules to see the rules of the server. Enjoy your stay " + user.Mention + " !");
        }

        private async Task _client_Log(LogMessage log)
        {
            Console.WriteLine(log.Message);
            await Task.Yield();
        }
    }
}