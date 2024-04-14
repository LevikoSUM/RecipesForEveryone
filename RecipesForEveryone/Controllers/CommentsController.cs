using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipesForEveryone.Data.Data;
using Services.DTOs;
using Services.Services.Abstractions;

namespace RecipesForEveryone.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ApplicationDbContext _context;

        public CommentsController(ICommentService commentService, ApplicationDbContext context)
        {
            _commentService = commentService;
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index(int recipeId)
        {
            var comments = await _commentService.GetCommentsByRecipeIdAsync(recipeId);
            return View(comments);
        }

        //// GET: Comments/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _commentService.GetCommentByIdAsync(id.Value);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Title");
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentDTO comment)
        {
            //if (ModelState.IsValid)
            //{
            comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            comment.UserName = User.Identity.Name.Split('@').FirstOrDefault();
            await _commentService.AddCommentAsync(comment);
            return RedirectToAction("Details", "Recipes", new { id = comment.RecipeId });
            //return RedirectToAction(nameof(Index));
            ////}
            //ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", comment.UserId);
            //ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", comment.RecipeId);
            //return View(comment);
        }
    }
}
