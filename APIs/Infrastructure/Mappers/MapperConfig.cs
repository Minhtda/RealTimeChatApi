﻿using Application.ViewModel.MessageModel;
using Application.ViewModel.PostModel;
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
            CreatePostMap();
            PostMap();
            ProductMap();
            UpdatePostMap();
            CreateMessageMap();
            UpdateMessageMap();
        }
        internal void CreateUserMap()
        {
            CreateMap<RegisterModel,User>().ReverseMap();
            CreateMap<UpdateUserProfileModel, User>()
                .ForMember(src => src.BirthDay, opt => opt.MapFrom(x => x.Birthday.ToDateTime(TimeOnly.MinValue)))
                .ReverseMap();  
        }
        internal void CreateProductMap()
        {
            CreateMap<CreateProductModel,Product>()
                .ForMember(src=>src.CategoryId,opt=>opt.MapFrom(x=>x.CategoryId))
                .ForMember(src=>src.ConditionId,opt=>opt.MapFrom(x=>x.ProductTypeId))
                .ReverseMap();   
        }
        internal void CreatePostMap()
        {
            CreateMap<CreatePostModel, Post>()
                .ReverseMap();
        }
        internal void PostMap()
        {
            CreateMap<PostModel, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.PostId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(x => x.ProductId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.PostId))
                .ReverseMap()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(x => x.Product));
        }

        internal void ProductMap()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.ConditionName, opt => opt.MapFrom(src => src.ConditionType.ConditionType))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();
        }
        internal void UpdatePostMap()
        {
            CreateMap<UpdatePostModel, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.PostId))
                .ReverseMap();
        }
        internal void UpdateMessageMap()
        {
            CreateMap<UpdateMessageModel, Message>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();
        }
        internal void CreateMessageMap()
        {
            CreateMap<CreateMessageModel, Message>()
                .ReverseMap();
        }
    }
}
