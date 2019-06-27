using BotCore.Models;
using HtmlAgilityPack;
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
                .FirstOrDefault().GetAttributeValue("href", "link not found"); ;

            await LoadHtmlAsync(firstRecipeLink);



            return new RecipeModel()
            {
                Name = firstRecipeLink
            };
        }

        private async Task LoadHtmlAsync(string url)
        {
            var htmlString = await _client.GetStringAsync(url);
            _html.LoadHtml(htmlString);
        }
    }
}
