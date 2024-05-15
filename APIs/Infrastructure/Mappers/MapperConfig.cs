using Application.ViewModel.CartModel;
using Application.ViewModel.UserModel;
using Application.ViewModel.UserViewModel;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateUserMap();
            CreateProductMap();
        }
        internal void CreateUserMap()
        {
            CreateMap<RegisterModel,User>().ReverseMap();
            CreateMap<UpdateUserProfileModel,User>().ReverseMap();  
        }
        internal void CreateProductMap()
        {
            CreateMap<Item,Product>().ReverseMap();
        }
    }
}
