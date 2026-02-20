using Application.Features.WorkoutAssignments.Commands.Create;
using Application.Features.WorkoutAssignments.Commands.Delete;
using Application.Features.WorkoutAssignments.Commands.Update;
using Application.Features.WorkoutAssignments.Queries.GetById;
using Application.Features.WorkoutAssignments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.WorkoutAssignments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<WorkoutAssignment, CreateWorkoutAssignmentCommand>().ReverseMap();
        CreateMap<WorkoutAssignment, CreatedWorkoutAssignmentResponse>().ReverseMap();
        CreateMap<WorkoutAssignment, UpdateWorkoutAssignmentCommand>().ReverseMap();
        CreateMap<WorkoutAssignment, UpdatedWorkoutAssignmentResponse>().ReverseMap();
        CreateMap<WorkoutAssignment, DeleteWorkoutAssignmentCommand>().ReverseMap();
        CreateMap<WorkoutAssignment, DeletedWorkoutAssignmentResponse>().ReverseMap();
        CreateMap<WorkoutAssignment, GetByIdWorkoutAssignmentResponse>().ReverseMap();
        CreateMap<WorkoutAssignment, GetListWorkoutAssignmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<WorkoutAssignment>, GetListResponse<GetListWorkoutAssignmentListItemDto>>().ReverseMap();
    }
}
