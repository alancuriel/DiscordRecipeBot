using Unity;

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

            _container.RegisterSingleton<ILogger,Logger>();
            _container.RegisterSingleton<Discord.Connection>();
            //_container.RegisterType<interface, implemtation>();
            //_container.RegisterSingleton<interface, implemtation>();
        }

        public static T Resolve<T>() => Container.Resolve<T>();

    }
}