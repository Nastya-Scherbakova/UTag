using AutoMapper;
using UTag.Models;
using UTag.ViewModels;

namespace UTag.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<FilterViewModel, Filter>();
            CreateMap<Filter, FilterViewModel>();
            CreateMap<TagViewModel, Tag>();
            CreateMap<Tag, TagViewModel>();
            CreateMap<PersonViewModel, Person>();
            CreateMap<Person, PersonViewModel>();
        }
    }
}