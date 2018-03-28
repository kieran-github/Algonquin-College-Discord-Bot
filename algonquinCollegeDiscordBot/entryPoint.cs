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

            await _client.StartAsync();
            await Task.Delay(-1);
            
        }



        private async Task _client_Log(LogMessage log)
        {
            Console.WriteLine(log.Message);
            await Task.Yield();
        }
    }
}