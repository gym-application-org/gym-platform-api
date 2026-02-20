using Application.Features.DietTemplateMeals.Commands.Create;
using Application.Features.DietTemplateMeals.Commands.Delete;
using Application.Features.DietTemplateMeals.Commands.Update;
using Application.Features.DietTemplateMeals.Queries.GetById;
using Application.Features.DietTemplateMeals.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.DietTemplateMeals.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DietTemplateMeal, CreateDietTemplateMealCommand>().ReverseMap();
        CreateMap<DietTemplateMeal, CreatedDietTemplateMealResponse>().ReverseMap();
        CreateMap<DietTemplateMeal, UpdateDietTemplateMealCommand>().ReverseMap();
        CreateMap<DietTemplateMeal, UpdatedDietTemplateMealResponse>().ReverseMap();
        CreateMap<DietTemplateMeal, DeleteDietTemplateMealCommand>().ReverseMap();
        CreateMap<DietTemplateMeal, DeletedDietTemplateMealResponse>().ReverseMap();
        CreateMap<DietTemplateMeal, GetByIdDietTemplateMealResponse>().ReverseMap();
        CreateMap<DietTemplateMeal, GetListDietTemplateMealListItemDto>().ReverseMap();
        CreateMap<IPaginate<DietTemplateMeal>, GetListResponse<GetListDietTemplateMealListItemDto>>().ReverseMap();
    }
}
