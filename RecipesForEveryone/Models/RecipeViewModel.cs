using Services.DTOs;

namespace RecipesForEveryone.Models
{
    public class RecipeViewModel : RecipeDTO
    {
        public IFormFile Picture { get; set; }
    }
}
