using BotCore.Discord;
using BotCore.Discord.Entities;
using BotCore.Discord.Handlers;
using BotCore.Storage;
using System;
using System.Threading.Tasks;

namespace BotCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Unity.RegisterTypes();
            Console.WriteLine("Hello World!");

            var storage = Unity.Resolve<IDataStorage>();

            var connection = Unity.Resolve<Connection>();
            await connection.ConnectAsync(new FoodBotConfig()
            {
                Token = storage.RestoreObject<string>("Config/BotToken")
            });

            await Unity.Resolve<ICommandHandler>().InitializeAsync();
            Unity.Resolve<IReactionHandler>().Initialize();

            await Task.Delay(-1);
        }
    }
}
