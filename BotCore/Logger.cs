using System;


namespace BotCore
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {  
            Console.WriteLine(message);
        }
    }
}
