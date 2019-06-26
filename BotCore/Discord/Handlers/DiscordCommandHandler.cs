using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Discord.Handlers
{
    public class DiscordCommandHandler : ICommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;

        public DiscordCommandHandler(DiscordSocketClient client, CommandService commandService)
        {
            _client = client;
            _commandService = commandService;
        }

        public async Task InitializeAsync()
        {
            
            _client.MessageReceived += HandleCommandAsync;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage msg))
            {
                return;
            }

            var argPos = 0;
            if (msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, msg);
                await TryRunAsBotCommandAsync(context, argPos).ConfigureAwait(false);
            }
        }

        private async Task TryRunAsBotCommandAsync(SocketCommandContext context, int argPos)
        {
            var result = await _commandService.ExecuteAsync(context, argPos, services : null);
            
            if(!result.IsSuccess)
            {
                Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
