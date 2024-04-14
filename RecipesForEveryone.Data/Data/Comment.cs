using RecipesForEveryone.Data.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesForEveryone.Data.Data
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public int RecipeId { get; set; }
        public string UserName { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
