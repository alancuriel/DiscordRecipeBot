using BotCore.Models;

namespace BotCore.Data.Scraper
{
    public interface IRecipeScraper
    {
        RecipeModel GetRecipe(string keyword);
    }
}