using Application.ViewModel.CartModel;
using Application.ViewModel.ProductModel;
using Application.ViewModel.UserModel;
using Application.ViewModel.UserViewModel;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Routing.Constraints;
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
            CreateMap<CreateProductModel,Product>()
                .ForMember(src=>src.CategoryId,opt=>opt.MapFrom(x=>x.CategoryId))
                .ForMember(src=>src.ProductTypeId,opt=>opt.MapFrom(x=>x.ProductTypeId))
                .ReverseMap();   
        }
    }
}
