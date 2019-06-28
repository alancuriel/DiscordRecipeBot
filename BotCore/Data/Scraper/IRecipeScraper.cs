using BotCore.Models;
using System.Threading.Tasks;

namespace BotCore.Data.Scraper
{
    public interface IRecipeScraper
    {
        Task<RecipeModel> GetRecipeAsync(string keyword);
        Task<System.Collections.Generic.List<RecipeLinkModel>> GetRecipeLinks(string keyword);
    }
}