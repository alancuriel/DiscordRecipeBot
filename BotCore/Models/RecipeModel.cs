using System;
using System.Collections.Generic;


namespace BotCore.Models
{
    public class RecipeModel
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Directions { get; set; }
        public TimeSpan Time { get; set; }
        public int Calories { get; set; }
        public string Img { get; set; }
        public string Link { get; set; }
    }
}
