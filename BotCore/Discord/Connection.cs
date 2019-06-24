using BotCore.Discord.Entities;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Discord
{
    public class Connection
    {
        private DiscordSocketClient _client;
        private DiscordLogger _logger;

        public Connection(DiscordLogger logger)
        {
            _logger = logger;

        }

        internal async Task ConnectAsync(FoodBotConfig config)
        {
            _client = new DiscordSocketClient(config.SocketConfig);

            _client.Log += _logger.Log;
        }
    }
}
