using System;
using System.Threading.Tasks;
using Discord;

namespace BotCore.Discord
{
    public class DiscordLogger
    {
        private ILogger _logger;
        public DiscordLogger(ILogger logger)
        {
            _logger = logger;

        }


        public Task Log(LogMessage logMsg)
        {
            _logger.Log(logMsg.Message);
            return Task.CompletedTask;
        }

        public void Test()
        {
            _logger.Log("hello again");
        }
    }
}
