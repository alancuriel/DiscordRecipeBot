using System.Threading.Tasks;

namespace BotCore.Discord
{
    public interface ICommandHandler
    {
        Task InitializeAsync();
    }
}
