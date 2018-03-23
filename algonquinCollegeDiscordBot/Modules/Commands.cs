using System.Threading.Tasks;
using Discord.Commands;
using System.IO;

namespace algonquinCollegeDiscordBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
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
                "\n" +
                "!commands\n" +
                "!invite\n" +
                "!rules\n" +
                "!sourcecode");

        }
        [Command("invite"), Summary("Displays the permanent invite link for the Algonquin College discord.")]
        public async Task Invite()
        {
            await Context.Channel.SendMessageAsync("https://discord.gg/VwukBWK");
        }

        [Command("sourcecode"), Summary("Displays the github repo for this discord bot.")]
        public async Task Sourcecode()
        {
            await Context.Channel.SendMessageAsync("github.com/kieran-github");
        }
    }
}
