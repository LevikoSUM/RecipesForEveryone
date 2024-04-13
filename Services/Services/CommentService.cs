using AutoMapper;
using RecipesForEveryone.Data.Data;
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
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _repository;
        private readonly IMapper _mapper;
        public CommentService(IRepository<Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddCommentAsync(CommentDTO comment)
        {
            var commentEntity = _mapper.Map<Comment>(comment);
            await _repository.AddAsync(commentEntity);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsByRecipeIdAsync(int recipeId)
        {
            var comments = await _repository.GetAsync(c => c.RecipeId == recipeId);
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public async Task UpdateCommentAsync(CommentDTO comment)
        {
            var commentEntity = _mapper.Map<Comment>(comment);
            await _repository.UpdateAsync(commentEntity);
        }
    }
}
