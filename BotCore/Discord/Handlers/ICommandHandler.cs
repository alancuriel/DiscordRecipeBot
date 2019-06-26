using System.Threading.Tasks;

namespace BotCore.Discord.Handlers
{
    public interface ICommandHandler
    {
        Task InitializeAsync();
    }
}
