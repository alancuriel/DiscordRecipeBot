using BotCore.Discord;
using BotCore.Storage;
using BotCore.Storage.Implementations;
using Discord.WebSocket;
using Unity;
using Unity.Injection;

namespace BotCore
{
    public static class Unity
    {
        private static UnityContainer _container;

        public static UnityContainer Container
        {
            get
            {
                if(_container == null)
                    RegisterTypes();
                
                return _container;
            }
        }

        public static void RegisterTypes()
        {
            _container = new UnityContainer();

            _container.RegisterSingleton<IDataStorage, JsonStorage>()
            .RegisterSingleton<ILogger,Logger>()
            .RegisterFactory<DiscordSocketConfig>(i => SocketConfig.GetDefault())      
            .RegisterSingleton<DiscordSocketClient>(new InjectionConstructor(typeof(DiscordSocketConfig)))
            .RegisterSingleton<Discord.Connection>()
            .RegisterSingleton<ICommandHandler, DiscordCommandHandler>();


            //_container.RegisterType<interface, implemtation>();
            //_container.RegisterSingleton<interface, implemtation>();
        }

        public static T Resolve<T>() => Container.Resolve<T>();

    }
}