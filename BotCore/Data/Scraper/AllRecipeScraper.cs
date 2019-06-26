using BotCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Data.Scraper
{
    public class AllRecipeScraper : IRecipeScraper
    {
        public RecipeModel GetRecipe(string keyword)
        {
            throw new NotImplementedException();//TODO: implement simple recipe search through Scraping from Allrecipes.com
        }
    }
}
