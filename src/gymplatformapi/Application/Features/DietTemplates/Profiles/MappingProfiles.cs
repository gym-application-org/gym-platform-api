using Application.Features.DietTemplates.Commands.Create;
using Application.Features.DietTemplates.Commands.Delete;
using Application.Features.DietTemplates.Commands.Update;
using Application.Features.DietTemplates.Queries.GetById;
using Application.Features.DietTemplates.Queries.GetById.Dtos;
using Application.Features.DietTemplates.Queries.GetList;
using Application.Features.Members.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.DietTemplates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DietTemplate, CreateDietTemplateCommand>()
            .ForMember(dest => dest.Days, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<DietTemplate, CreatedDietTemplateResponse>();
        CreateMap<DietTemplate, UpdateDietTemplateCommand>()
            .ForMember(dest => dest.Days, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<DietTemplate, UpdatedDietTemplateResponse>();
        CreateMap<DietTemplate, DeleteDietTemplateCommand>().ReverseMap();
        CreateMap<DietTemplate, DeletedDietTemplateResponse>();
        CreateMap<DietTemplate, GetByIdDietTemplateResponse>();
        CreateMap<DietTemplateDay, GetByIdDietTemplateDayDto>();
        CreateMap<DietTemplateMeal, GetByIdDietTemplateMealDto>();
        CreateMap<DietTemplateMealItem, GetByIdDietTemplateMealItemDto>();
        CreateMap<DietTemplate, GetListDietTemplateListItemDto>()
            .ForMember(dest => dest.DayCount, opt => opt.MapFrom(src => src.Days.Count));
        CreateMap<IPaginate<DietTemplate>, GetListResponse<GetListDietTemplateListItemDto>>();
    }
}
