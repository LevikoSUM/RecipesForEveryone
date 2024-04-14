using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipesForEveryone.Data.Data;
using RecipesForEveryone.Data.Data.Enums;
using RecipesForEveryone.Models;
using RecipesForEveryone.Utils;
using Services.DTOs;
using Services.Services.Abstractions;

namespace RecipesForEveryone.Controllers
{
    [Authorize]
    public class PublicRecipesController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public PublicRecipesController(IRecipeService recipeService, IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _recipeService = recipeService;
            _environment = environment;
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
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
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", recipe.UserId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Ingredients,Instructions,IsPublic,Image,UserId,RecipeType,Id")] RecipeDTO recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            }
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", recipe.UserId);
            return View(recipe);
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
