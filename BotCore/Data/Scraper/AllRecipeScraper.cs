using BotCore.Models;
using HtmlAgilityPack;
using System;
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

        public async Task<List<RecipeLinkModel>> GetRecipeLinks(string keyword)
        {
            var searchString = GetDefaultSearchString(keyword);
            await LoadHtmlAsync(searchString);


            var recipeCartTitles = _html.DocumentNode.Descendants("a")
                                    .Where(n => n.GetAttributeValue("class"," ") == "fixed-recipe-card__title-link").ToList();

            if(recipeCartTitles.Count <= 0)
                throw new Exception($"No recipes found with the phrase {keyword}");

            var recipeLinks = recipeCartTitles.ConvertAll(node =>
            new RecipeLinkModel
            {
                Url = node.GetAttributeValue("href", "not found"),
                Name = node.Descendants("span").ToList().First().GetDirectInnerText()
            });

            return recipeLinks;
        }

        private string GetDefaultSearchString(string keyword)
        {
            return $"https://www.allrecipes.com/search/results/?wt={keyword}&sort=re";
        }

        public async Task<RecipeModel> GetRecipeAsync(String keyword) {
            var searchString = GetDefaultSearchString(keyword);

            await LoadHtmlAsync(searchString);


            var recipeCardList = GetRecipeCardList();

            if (recipeCardList.Count == 0)
                throw new Exception($"No recipes found with the phrase {keyword}");

            var firstRecipeLink = recipeCardList[0].Descendants("a")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("fixed-recipe-card__title-link"))
                .FirstOrDefault().GetAttributeValue("href", "link not found");
                var model = await Refactor(firstRecipeLink);
                return model;
        }

        public Task<RecipeModel> GetRecipeLinkAsync(String keyword) {
            return Refactor(keyword);
        }

        public async Task<RecipeModel> Refactor(string urlString)
        {
            
            await LoadHtmlAsync(urlString);

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
                .StartsWith("rec-photo")).ToList();

            var firstImageLink = recipeImageList[0]?.GetAttributeValue("src", "image not found"); ;

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

            var recipeCalories = _html.DocumentNode.Descendants("span")
            .Where(node => node.GetAttributeValue("class","") == "calorie-count").ToArray().First()
            .Descendants("span").Where(node => node.GetAttributeValue("class","na") == "na").First().GetDirectInnerText();
            
            Int32.TryParse(recipeCalories, out int cal);

            var recipeTime = _html.DocumentNode.Descendants("span")
            .Where(node => node.GetAttributeValue("class"," ") == "ready-in-time")
            .ToList().First().GetDirectInnerText();


            return new RecipeModel()
            {
                Name = recipeTitle,
                Ingredients = recipeIngredientsList,
                Directions = recipeInstructionsList,
                Link = urlString,
                Calories = cal,
                Img = firstImageLink,
                Time = recipeTime
            };
        }

        private async Task LoadHtmlAsync(string url)
        {
            var htmlString = await _client.GetStringAsync(url);
            _html.LoadHtml(htmlString);
        }

        private List<HtmlNode> GetRecipeCardList()
        {
            return _html.DocumentNode.Descendants("article")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("fixed-recipe-card")).ToList();
        }
    }
}
