﻿using RecipesForEveryone.Data.Data.Abstractions;
using RecipesForEveryone.Data.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecipesForEveryone.Data.Data
{
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public bool IsPublic { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
        public virtual RecipeType RecipeType { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
