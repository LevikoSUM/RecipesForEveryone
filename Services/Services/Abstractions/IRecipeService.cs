using RecipesForEveryone.Data.Data;
using RecipesForEveryone.Data.Data.Enums;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Abstractions
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDTO>> GetAllRecipesAsync();
        Task<RecipeDTO> GetRecipeByIdAsync(int id);
        Task<IEnumerable<RecipeDTO>> GetRecipesByUserIdAsync(string userId);
        Task<IEnumerable<RecipeDTO>> GetRecipesByTypeAsync(RecipeType recipeType); 
        Task AddRecipeAsync(RecipeDTO recipe);
        Task UpdateRecipeAsync(RecipeDTO recipe);
        Task DeleteRecipeAsync(int id);
    }
}
