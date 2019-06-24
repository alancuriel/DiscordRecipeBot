using BotCore.Discord;
using BotCore.Discord.Entities;
using System;

namespace BotCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var BotConfiguration = new FoodBotConfig()
            {
                Token = "bot token",
                SocketConfig = SocketConfig.GetDefault()
            };

        }
    }
}
