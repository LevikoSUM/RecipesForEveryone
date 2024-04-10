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
        public string UserID { get; set; }
        public int RecipeID { get; set; }

        public virtual AppUser? User { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
