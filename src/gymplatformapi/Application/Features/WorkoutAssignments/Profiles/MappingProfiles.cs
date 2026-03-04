using Application.Features.WorkoutAssignments.Commands.Create;
using Application.Features.WorkoutAssignments.Commands.Delete;
using Application.Features.WorkoutAssignments.Commands.Update;
using Application.Features.WorkoutAssignments.Queries.GetById;
using Application.Features.WorkoutAssignments.Queries.GetList;
using Application.Features.WorkoutAssignments.Queries.GetMyWorkoutAssignmentList;
using Application.Features.WorkoutAssignments.Queries.GetMyWorkoutById;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using OtpNet;

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
        CreateMap<WorkoutAssignment, GetMyListWorkoutAssignmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<WorkoutAssignment>, GetListResponse<GetMyListWorkoutAssignmentListItemDto>>().ReverseMap();
        CreateMap<WorkoutAssignment, GetMyWorkoutByIdResponse>()
            .ForMember(dest => dest.AssignmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.WorkoutTemplateId, opt => opt.MapFrom(src => src.WorkoutTemplateId))
            .ForMember(dest => dest.WorkoutTemplateName, opt => opt.MapFrom(src => src.WorkoutTemplate.Name))
            .ForMember(dest => dest.WorkoutTemplateIsActive, opt => opt.MapFrom(src => src.WorkoutTemplate.IsActive))
            .ForMember(dest => dest.WorkoutTemplateLevel, opt => opt.MapFrom(src => src.WorkoutTemplate.Level))
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.WorkoutTemplate.Days));

        CreateMap<WorkoutTemplateDay, MyWorkoutTemplateDayDto>();
        CreateMap<WorkoutTemplateDayExercise, MyWorkoutTemplateDayExerciseDto>();
    }
}
