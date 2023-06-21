using AutoMapper;

using Share.Models;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Hepers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Menu, MenuVM>().ReverseMap();
            CreateMap<ProductVM, Product>().ReverseMap();
            CreateMap<BrandVM, Brand>().ReverseMap();
            CreateMap<ContactVM, Contact>().ForMember(t => t.ContactId, opt => opt.Ignore());
            CreateMap<AboutVM, About>().ReverseMap();
            CreateMap<CustomerVM, Customer>().ReverseMap();
            CreateMap<SupplierVM, Supplier>().ReverseMap();
            CreateMap<PostVM, Post>().ReverseMap();
            CreateMap<PostCategoryVM, PostCategory>().ReverseMap();
            CreateMap<PostCommentVM, PostComment>().ReverseMap();
            CreateMap<ProductCategoryVM, ProductCategory>().ReverseMap();
            CreateMap<ProductCommentVM, ProductComment>().ReverseMap();
            CreateMap<SliderVM, Slider>().ReverseMap();
            CreateMap<OrderVM, Order>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailVM>().ReverseMap();
        }
    }
}
