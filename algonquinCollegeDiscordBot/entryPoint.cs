using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;


namespace algonquinCollegeDiscordBot
{
    public class EntryPoint
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public static void Main(string[] args) => new EntryPoint().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _handler = new CommandHandler(_client);

            await _client.LoginAsync(TokenType.Bot, creds.token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }
    }
}