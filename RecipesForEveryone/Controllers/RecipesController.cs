using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipesForEveryone.Data;
using RecipesForEveryone.Data.Data;
using RecipesForEveryone.Data.Data.Enums;
using RecipesForEveryone.Models;
using RecipesForEveryone.Utils;
using Services.DTOs;
using Services.Services;
using Services.Services.Abstractions;
using ApplicationDbContext = RecipesForEveryone.Data.Data.ApplicationDbContext;

namespace RecipesForEveryone.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public RecipesController(IRecipeService recipeService, IWebHostEnvironment environment, ApplicationDbContext context )
        {
            _recipeService = recipeService;
            _environment = environment;
            _context = context;

        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {            
            return View(await _recipeService.GetRecipesByUserIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        public async Task<IActionResult> PublicIndex()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            var publicRecipes = recipes.Where(r => r.IsPublic);
            return View(publicRecipes);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _recipeService.GetRecipeByIdAsync(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var recipe = await _recipeService.GetRecipeByIdAsync(id.Value);
            //if (recipe == null)
            //{
            //    return NotFound();
            //}

            //return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            var recipeTypes = Enum.GetValues(typeof(RecipeType))
                          .Cast<RecipeType>()
                          .Select(r => new SelectListItem
                          {
                              Text = r.ToString(),
                              Value = ((int)r).ToString()
                          })
                          .ToList();

            ViewBag.RecipeTypes = recipeTypes;
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeViewModel recipe)
        {
            //if (ModelState.IsValid)
            //{
                if (recipe.Picture != null && recipe.Picture.Length > 0)
                {
                    var newFileName = await FileUpload.UploadAsync(recipe.Picture, _environment.WebRootPath);
                    recipe.Image = newFileName;
                }
                recipe.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _recipeService.AddRecipeAsync(recipe); 
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", recipe.UserId);
            //return View(recipe);
        }

        // GET: Recipes/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _recipeService.GetRecipeByIdAsync(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }
            var viewModel = new RecipeViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                IsPublic = recipe.IsPublic,
                Image = recipe.Image,
                UserId = recipe.UserId,
                RecipeType = recipe.RecipeType,
                RecipeTypeName = recipe.RecipeTypeName,
                Comments = recipe.Comments
            };
            var recipeTypes = Enum.GetValues(typeof(RecipeType))
                          .Cast<RecipeType>()
                          .Select(r => new SelectListItem
                          {
                              Text = r.ToString(),
                              Value = ((int)r).ToString()
                          })
                          .ToList();
            ViewBag.RecipeTypes = recipeTypes;
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", recipe.UserId);
            return View(viewModel);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecipeViewModel recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    // Handle picture upload if a new picture is provided
                    if (recipe.Picture != null && recipe.Picture.Length > 0)
                    {
                        var newFileName = await FileUpload.UploadAsync(recipe.Picture, _environment.WebRootPath);
                        recipe.Image = newFileName;
                    }

                    // Update user ID
                    recipe.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    await _recipeService.UpdateRecipeAsync(recipe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}

            //ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", recipe.UserId);
            //return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _recipeService.GetRecipeByIdAsync(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            await _recipeService.DeleteRecipeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RecipeExists(int id)
        {
          return await _recipeService.GetRecipeByIdAsync(id) != null;
        }
    }
}
