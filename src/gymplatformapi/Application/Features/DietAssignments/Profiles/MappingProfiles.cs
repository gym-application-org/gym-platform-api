using Application.Features.DietAssignments.Commands.Create;
using Application.Features.DietAssignments.Commands.CreateBulk;
using Application.Features.DietAssignments.Commands.Delete;
using Application.Features.DietAssignments.Commands.Update;
using Application.Features.DietAssignments.Queries.GetById;
using Application.Features.DietAssignments.Queries.GetList;
using Application.Features.DietAssignments.Queries.GetMyDietAssignemnts;
using Application.Features.DietAssignments.Queries.GetMyDietAssignmentById;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.DietAssignments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DietAssignment, CreateDietAssignmentCommand>().ReverseMap();
        CreateMap<DietAssignment, CreatedDietAssignmentResponse>().ReverseMap();
        CreateMap<DietAssignment, CreateBulkDietAssignmentCommand>().ReverseMap();
        CreateMap<DietAssignment, CreatedBulkDietAssignmentResponse>().ReverseMap();
        CreateMap<DietAssignment, UpdateDietAssignmentCommand>().ReverseMap();
        CreateMap<DietAssignment, UpdatedDietAssignmentResponse>().ReverseMap();
        CreateMap<DietAssignment, DeleteDietAssignmentCommand>().ReverseMap();
        CreateMap<DietAssignment, DeletedDietAssignmentResponse>().ReverseMap();
        CreateMap<DietAssignment, GetByIdDietAssignmentResponse>().ReverseMap();
        CreateMap<DietAssignment, GetListDietAssignmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<DietAssignment>, GetListResponse<GetListDietAssignmentListItemDto>>().ReverseMap();
        CreateMap<DietAssignment, GetMyDietAssignmentsListItemDto>()
            .ForMember(dest => dest.AssignmentId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();
        CreateMap<IPaginate<DietAssignment>, GetListResponse<GetMyDietAssignmentsListItemDto>>().ReverseMap();

        CreateMap<DietAssignment, GetMyDietAssignmentByIdResponse>()
            .ForMember(dest => dest.AssignmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DietTemplateId, opt => opt.MapFrom(src => src.DietTemplateId))
            .ForMember(dest => dest.DietTemplateName, opt => opt.MapFrom(src => src.DietTemplate.Name))
            .ForMember(dest => dest.DietTemplateDescription, opt => opt.MapFrom(src => src.DietTemplate.Description))
            .ForMember(dest => dest.CaloriesTarget, opt => opt.MapFrom(src => src.DietTemplate.CaloriesTarget))
            .ForMember(dest => dest.ProteinGramsTarget, opt => opt.MapFrom(src => src.DietTemplate.ProteinGramsTarget))
            .ForMember(dest => dest.CarbGramsTarget, opt => opt.MapFrom(src => src.DietTemplate.CarbGramsTarget))
            .ForMember(dest => dest.FatGramsTarget, opt => opt.MapFrom(src => src.DietTemplate.FatGramsTarget))
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.DietTemplate.Days));

        CreateMap<DietTemplateDay, MyDietTemplateDayDto>();
        CreateMap<DietTemplateMeal, MyDietTemplateMealDto>();
        CreateMap<DietTemplateMealItem, MyDietTemplateMealItemDto>();
    }
}
