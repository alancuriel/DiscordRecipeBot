using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Discord.Modules
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong!");
            
        }

        [Command("hello")]
        public async Task EmbededWorldAsync()
        {
            var embed = new EmbedBuilder
            {
                // Embed property can be set within object initializer
                Title = "Hello world!",
                Description = "I am a description set by initializer."
            };
            // Or with methods
            embed.AddField("Field title",
                "Field value. I also support [hyperlink markdown](https://example.com)!")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer => footer.Text = "I am a footer.")
                .WithColor(Color.Blue)
                .WithTitle("I overwrote \"Hello world!\"")
                .WithUrl("https://example.com")
                .WithDescription("I am a description.")
                .WithCurrentTimestamp();
                

            await ReplyAsync(null, false, embed.Build());
        }

        [Command("echo")]
        public async Task EchoAsync([Remainder]string message)
        {
            await ReplyAsync(message);
        }
    }
}
