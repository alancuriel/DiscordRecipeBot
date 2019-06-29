using BotCore.Discord.Services;
using BotCore.Models;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotCore.Discord.Modules
{
    public class RecipeModule : ModuleBase<SocketCommandContext>
    {
        private readonly RecipeService _recipeService;

        
        public RecipeModule()
        {  
            _recipeService = Unity.Resolve<RecipeService>();
        }

        [Command("recipe")]
        [Alias("r")]
        public async Task SearchRecipeAsync([Remainder] string r)
        {

            EmbedBuilder embed;
            try
            {
                var recipe = await _recipeService.SearchForRecipe(r);

                embed = new EmbedBuilder
                {
                    Title = recipe.Name,
                    Url = recipe.Link,
                    ImageUrl = recipe.Img,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = $"Calories: {recipe.Calories.ToString()}  Time: {recipe.Time}"
                    },
                    Description = ParseIngredients(recipe.Ingredients),
                    Fields = ParseDirections(recipe.Directions)
                   
                };
            }
            catch(Exception ex)
            {

                embed = CreateErrorEmbed(ex.Message);
            }


            await ReplyAsync(null, false , embed.Build());   
        }

        [Command("top")]
        [Alias("tr")]
        public async Task SearchMoreRecipesAsync([Remainder] string r)
        {
            EmbedBuilder embed;
            string str = null;
            try
            {
                var recipeLinks = await _recipeService.SearchForRecipeLinks(r);

                str = ParseRecipeLinks(recipeLinks);

                embed = new EmbedBuilder
                {
                    Title = $"Recipe Results for {r}",
                    Description = str
                };
            }
            catch (Exception ex)
            {
                embed = CreateErrorEmbed(ex.Message);
            }
            
            
            var response  = await ReplyAsync(null,false,embed.Build());

            var occr = str.ToCharArray().Count(c => c == '\n');

            for(int i = 0; i < occr+1; i++)
            {
                switch(i)
                {
                    case (1):
                       await response.AddReactionAsync(new Emoji("1⃣"));
                        break;
                    case (2):
                        await response.AddReactionAsync(new Emoji("2⃣"));
                        break;
                    case (3):
                        await response.AddReactionAsync(new Emoji("3⃣"));
                        break;
                    case (4):
                        await response.AddReactionAsync(new Emoji("4⃣"));
                        break;
                    case (5):
                        await response.AddReactionAsync(new Emoji("5⃣"));
                        break;
                    default:
                        break;
                }
            }
        }

        private EmbedBuilder CreateErrorEmbed(string message)
        {
            return new EmbedBuilder
            {
                Title = ":x: Recipe not found!",
                Description = $"Reason: {message}"
            };
        }

        private string ParseRecipeLinks(List<RecipeLinkModel> recipeLinks)
        {
            string ings = "**Recipes**";

            for(int i = 0; i < recipeLinks.Count && i < 5; i++)
            {
                ings = $"{ings} \n{i+1}. [{recipeLinks[i].Name}]({recipeLinks[i].Url})";
            }

            return ings;
        }

        private List<EmbedFieldBuilder> ParseDirections(List<string> directions)
        {
            var dirs = new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder
                {
                    Name = "Directions",
                    Value = "----"
                }
            };

            for (int i = 0; i < directions.Count; i++)
            {
                if (i > 24)
                    break;

                dirs.Add(new EmbedFieldBuilder
                {
                    IsInline = true,
                    Name = $"{i+1}. ",
                    Value = directions[i]
                });
                
            }

            return dirs;
        }

        private string ParseIngredients(List<string> ingredients)
        {
            string ings = "**Ingredients**";

            foreach(var ingredient in ingredients)
            {
                ings = $"{ings} \n{ingredient}";
            }

            return ings;
        }

        

    }
}
