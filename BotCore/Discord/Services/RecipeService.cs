using BotCore.Data.Scraper;
using BotCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotCore.Discord.Services
{
    public class RecipeService
    {
        private readonly IRecipeScraper _recipeScraper;

        public RecipeService(IRecipeScraper recipeScraper)
        {
            _recipeScraper = recipeScraper;
        }

        public RecipeModel SearchForRecipe(string keyword)
        {
            return _recipeScraper.GetRecipe(keyword: keyword);
        }

    }
}
