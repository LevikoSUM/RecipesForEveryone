using RecipesForEveryone.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class CommentDTO : BaseDTO
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public int RecipeId { get; set; }
        public string UserName { get; set; }
    }
}
