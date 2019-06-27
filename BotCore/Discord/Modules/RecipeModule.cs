using BotCore.Discord.Services;
using Discord.Commands;
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
            
            await ReplyAsync(recipe.Name);
        }
    }
}
