using Application.Features.DietTemplateDays.Commands.Create;
using Application.Features.DietTemplateDays.Commands.Delete;
using Application.Features.DietTemplateDays.Commands.Update;
using Application.Features.DietTemplateDays.Queries.GetById;
using Application.Features.DietTemplateDays.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.DietTemplateDays.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DietTemplateDay, CreateDietTemplateDayCommand>().ReverseMap();
        CreateMap<DietTemplateDay, CreatedDietTemplateDayResponse>().ReverseMap();
        CreateMap<DietTemplateDay, UpdateDietTemplateDayCommand>().ReverseMap();
        CreateMap<DietTemplateDay, UpdatedDietTemplateDayResponse>().ReverseMap();
        CreateMap<DietTemplateDay, DeleteDietTemplateDayCommand>().ReverseMap();
        CreateMap<DietTemplateDay, DeletedDietTemplateDayResponse>().ReverseMap();
        CreateMap<DietTemplateDay, GetByIdDietTemplateDayResponse>().ReverseMap();
        CreateMap<DietTemplateDay, GetListDietTemplateDayListItemDto>().ReverseMap();
        CreateMap<IPaginate<DietTemplateDay>, GetListResponse<GetListDietTemplateDayListItemDto>>().ReverseMap();
    }
}
