using BotCore.Data.Scraper;
using BotCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Discord.Services
{
    public class RecipeService
    {
        private readonly IRecipeScraper _recipeScraper;

        public RecipeService(IRecipeScraper recipeScraper)
        {
            _recipeScraper = recipeScraper;
        }

        public Task<RecipeModel> SearchForRecipe(string keyword)
        {
            return  _recipeScraper.GetRecipeAsync(keyword: keyword);
        }

        public Task<List<RecipeLinkModel>> SearchForRecipeLinks(string keyword)
        {
            return _recipeScraper.GetRecipeLinks(keyword: keyword);
        }

    }
}
