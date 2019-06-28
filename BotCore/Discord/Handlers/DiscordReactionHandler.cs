using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Discord.Handlers
{
    public class DiscordReactionHandler : IReactionHandler
    {
        private readonly DiscordSocketClient _client;

        public DiscordReactionHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public void Initialize()
        {
            _client.ReactionAdded += HandleReactionAsync;
        }

        private async Task HandleReactionAsync(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel channel, SocketReaction reaction)
        {
            var message = cache.Value;

            if (!(message.Author.Id == _client.CurrentUser.Id && message != null))
                return;

            

            if(reaction.Emote.Name == "❤")
            {
                
            }

            
        }
    }
}
