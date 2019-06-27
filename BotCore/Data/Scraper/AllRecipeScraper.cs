using BotCore.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Data.Scraper
{
    public class AllRecipeScraper : IRecipeScraper
    {
        private readonly HttpClient _client;
        private readonly HtmlDocument _html;

        public AllRecipeScraper(HttpClient client, HtmlDocument html)
        {
            _client = client;
            _html = html;
        }

        public async Task<RecipeModel> GetRecipeAsync(string keyword)
        {
            var searchString = $"https://www.allrecipes.com/search/results/?wt={keyword}&sort=re";
            await LoadHtmlAsync(searchString);


            var recipeCardList = _html.DocumentNode.Descendants("article")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("fixed-recipe-card")).ToList();

            var firstRecipeLink = recipeCardList[0].Descendants("a")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("fixed-recipe-card__title-link"))
                .FirstOrDefault().GetAttributeValue("href", "link not found");

            await LoadHtmlAsync(firstRecipeLink);

            var recipeTitleList = _html.DocumentNode.Descendants("section")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("recipe-summary clearfix"))
                .ToList();
            
            var recipeTitle = recipeTitleList[0].Descendants("h1")
                .Where(node => node.GetAttributeValue("id", "")
                .Equals("recipe-main-content"))
                .FirstOrDefault().InnerText;

            var recipeIngredientsElementList = _html.DocumentNode.Descendants("label")
                .Where(node => node.GetAttributeValue("ng-class", "")
                .Equals("{true: 'checkList__item'}[true]"))
                .ToList();

            var recipeIngredientsList = new List<string>();

            foreach(var item in recipeIngredientsElementList)
            {
              var thing = item.GetAttributeValue("title","");
              if(thing != "" && thing != null)
              {
                  recipeIngredientsList.Add(thing);
              }
              

            }

            var recipeImageList = _html.DocumentNode.Descendants("img")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("rec-photo")).ToList();

            var firstImageLink = recipeImageList[0].GetAttributeValue("src", "image not found"); ;

            var recipeInstructionsElementList = _html.DocumentNode.Descendants("span")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("recipe-directions__list--item"))
                .ToList();
            
            var recipeInstructionsList = new List<string>();

            foreach(var item in recipeInstructionsElementList)
            {
              var thing = item.InnerText;
              if(thing != "" && thing != null)
              {
                  recipeInstructionsList.Add(thing);
              }

            }





            return new RecipeModel()
            {
                Name = recipeTitle,
                Ingredients = recipeIngredientsList,
                Directions = recipeInstructionsList,
                Link = firstRecipeLink,
                Calories = 10,
                Img = firstImageLink
                
            };
        }

        private async Task LoadHtmlAsync(string url)
        {
            var htmlString = await _client.GetStringAsync(url);
            _html.LoadHtml(htmlString);
        }
    }
}
