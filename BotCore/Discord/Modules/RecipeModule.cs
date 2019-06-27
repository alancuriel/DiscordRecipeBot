using BotCore.Discord.Services;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
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
        public async Task SearchRecipeAsync([Remainder] string r)
        {
            var recipe = await _recipeService.SearchForRecipe(r);

            var embed = new EmbedBuilder
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


            await ReplyAsync(null, false , embed.Build());
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
