using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetCommentsByRecipeIdAsync(int recipeId);
        Task AddCommentAsync(CommentDTO commentDto);
        Task UpdateCommentAsync(CommentDTO commentDto);
        Task DeleteCommentAsync(int id);
    }
}
