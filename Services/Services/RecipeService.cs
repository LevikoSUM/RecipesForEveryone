using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipesForEveryone.Data.Data;
using RecipesForEveryone.Data.Data.Enums;
using RecipesForEveryone.Data.Repositories.Abstractions;
using Services.DTOs;
using Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _repository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public RecipeService(IRepository<Recipe> repository, IMapper mapper, ApplicationDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }
        public async Task AddRecipeAsync(RecipeDTO recipe)
        {
            var recipeEntity = _mapper.Map<Recipe>(recipe);
            await _repository.AddAsync(recipeEntity);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<RecipeDTO>> GetAllRecipesAsync()
        {
            var recipes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
        }

        public async Task<RecipeDTO> GetRecipeByIdAsync(int id)
        {
            var recipe = await _repository.GetByIdAsync(id);
            if (recipe != null)
            {
                await _context.Entry(recipe)
                    .Collection(r => r.Comments)
                    .LoadAsync();
            }
            return _mapper.Map<RecipeDTO>(recipe);
            //return _mapper.Map<RecipeDTO>
            //    (await _repository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<RecipeDTO>> GetRecipesByTypeAsync(RecipeType recipeType)
        {
            var recipes = await _repository.GetAsync(r => r.RecipeType == recipeType);
            return _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
        }

        public async Task<IEnumerable<RecipeDTO>> GetRecipesByUserIdAsync(string userId)
        {
            var recipes = await _repository.GetAsync(r => r.UserId == userId);
            return _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
        }

        public async Task UpdateRecipeAsync(RecipeDTO recipe)
        {
            var recipeEntity = _mapper.Map<Recipe>(recipe);
            await _repository.UpdateAsync(recipeEntity);
        }
    }
}
