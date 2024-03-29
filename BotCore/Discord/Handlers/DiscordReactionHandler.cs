﻿using BotCore.Discord.Services;
using BotCore.Models;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotCore.Discord.Handlers
{
    public class DiscordReactionHandler : IReactionHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly RecipeService _recipeService;

        public DiscordReactionHandler(DiscordSocketClient client, RecipeService recipeService)
        {
            _client = client;
            _recipeService = recipeService;
        }

        public void Initialize()
        {
            _client.ReactionAdded += HandleReactionAsync;
        }

        private async Task HandleReactionAsync(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel channel, SocketReaction reaction)
        {
            var message = cache.Value;

            if (!(message != null && message.Author.Id == _client.CurrentUser.Id))
                return;

            if (reaction.User.Value.IsBot)
                return;

            var msgEmbed = message.Embeds.GetEnumerator();
            msgEmbed.MoveNext();

            Console.WriteLine(msgEmbed.Current.Title);

            if (!(msgEmbed.Current.Title.StartsWith("Recipe Results for")))
                return;

            var linkString = msgEmbed.Current.Description.Split("\n");

            RecipeModel recipe = null;

            switch(reaction.Emote.Name)
            {
                case "1⃣":
                    if(linkString.Length >= 2)
                        recipe = await _recipeService.GetRecipeFromPageAsync(url: Regex.Match(linkString[1], @"\(([^)]*)\)").Groups[1].Value);
                    break;
                case "2⃣":
                    if (linkString.Length >= 3)
                        recipe = await _recipeService.GetRecipeFromPageAsync(url: Regex.Match(linkString[2], @"\(([^)]*)\)").Groups[1].Value);
                    break;
                case "3⃣":
                    if (linkString.Length >= 4)
                        recipe = await _recipeService.GetRecipeFromPageAsync(url: Regex.Match(linkString[3], @"\(([^)]*)\)").Groups[1].Value);
                    break;
                case "4⃣":
                    if (linkString.Length >= 5)
                        recipe = await _recipeService.GetRecipeFromPageAsync(url: Regex.Match(linkString[4], @"\(([^)]*)\)").Groups[1].Value);
                    break;
                case "5⃣":
                    if (linkString.Length >= 6)
                        recipe = await _recipeService.GetRecipeFromPageAsync(url: Regex.Match(linkString[5], @"\(([^)]*)\)").Groups[1].Value);
                    break;
                default:
                    return;
            }

            if (recipe == null)
                return;

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

            await channel.SendMessageAsync(null, false, embed.Build());
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
                    Name = $"{i + 1}. ",
                    Value = directions[i]
                });

            }

            return dirs;
        }

        private string ParseIngredients(List<string> ingredients)
        {
            string ings = "**Ingredients**";

            foreach (var ingredient in ingredients)
            {
                ings = $"{ings} \n{ingredient}";
            }

            return ings;
        }
    }
}
