using Application.Features.DietTemplateMealItems.Commands.Create;
using Application.Features.DietTemplateMealItems.Commands.Delete;
using Application.Features.DietTemplateMealItems.Commands.Update;
using Application.Features.DietTemplateMealItems.Queries.GetById;
using Application.Features.DietTemplateMealItems.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.DietTemplateMealItems.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DietTemplateMealItem, CreateDietTemplateMealItemCommand>().ReverseMap();
        CreateMap<DietTemplateMealItem, CreatedDietTemplateMealItemResponse>().ReverseMap();
        CreateMap<DietTemplateMealItem, UpdateDietTemplateMealItemCommand>().ReverseMap();
        CreateMap<DietTemplateMealItem, UpdatedDietTemplateMealItemResponse>().ReverseMap();
        CreateMap<DietTemplateMealItem, DeleteDietTemplateMealItemCommand>().ReverseMap();
        CreateMap<DietTemplateMealItem, DeletedDietTemplateMealItemResponse>().ReverseMap();
        CreateMap<DietTemplateMealItem, GetByIdDietTemplateMealItemResponse>().ReverseMap();
        CreateMap<DietTemplateMealItem, GetListDietTemplateMealItemListItemDto>().ReverseMap();
        CreateMap<IPaginate<DietTemplateMealItem>, GetListResponse<GetListDietTemplateMealItemListItemDto>>().ReverseMap();
    }
}
