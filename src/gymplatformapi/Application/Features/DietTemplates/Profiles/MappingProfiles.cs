using Application.Features.DietTemplates.Commands.Create;
using Application.Features.DietTemplates.Commands.Delete;
using Application.Features.DietTemplates.Commands.Update;
using Application.Features.DietTemplates.Queries.GetById;
using Application.Features.DietTemplates.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.DietTemplates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DietTemplate, CreateDietTemplateCommand>().ReverseMap();
        CreateMap<DietTemplate, CreatedDietTemplateResponse>().ReverseMap();
        CreateMap<DietTemplate, UpdateDietTemplateCommand>().ReverseMap();
        CreateMap<DietTemplate, UpdatedDietTemplateResponse>().ReverseMap();
        CreateMap<DietTemplate, DeleteDietTemplateCommand>().ReverseMap();
        CreateMap<DietTemplate, DeletedDietTemplateResponse>().ReverseMap();
        CreateMap<DietTemplate, GetByIdDietTemplateResponse>().ReverseMap();
        CreateMap<DietTemplate, GetListDietTemplateListItemDto>().ReverseMap();
        CreateMap<IPaginate<DietTemplate>, GetListResponse<GetListDietTemplateListItemDto>>().ReverseMap();
    }
}
