using RecipesForEveryone.Data.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class RecipeDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public bool IsPublic { get; set; }
        public string? Image { get; set; }
        public string UserId { get; set; }
        public RecipeType RecipeType { get; set; }
        public string RecipeTypeName { get; set; } 
    }
}
