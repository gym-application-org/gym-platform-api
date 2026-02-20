using Application.Features.DietAssignments.Commands.Create;
using Application.Features.DietAssignments.Commands.Delete;
using Application.Features.DietAssignments.Commands.Update;
using Application.Features.DietAssignments.Queries.GetById;
using Application.Features.DietAssignments.Queries.GetList;
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
        CreateMap<DietAssignment, UpdateDietAssignmentCommand>().ReverseMap();
        CreateMap<DietAssignment, UpdatedDietAssignmentResponse>().ReverseMap();
        CreateMap<DietAssignment, DeleteDietAssignmentCommand>().ReverseMap();
        CreateMap<DietAssignment, DeletedDietAssignmentResponse>().ReverseMap();
        CreateMap<DietAssignment, GetByIdDietAssignmentResponse>().ReverseMap();
        CreateMap<DietAssignment, GetListDietAssignmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<DietAssignment>, GetListResponse<GetListDietAssignmentListItemDto>>().ReverseMap();
    }
}
