using AutoMapper;
using RecipesForEveryone.Data.Data;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class RecipeProfile : Profile
    {
       public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDTO>()
                .ReverseMap();
        } 
    }
}
