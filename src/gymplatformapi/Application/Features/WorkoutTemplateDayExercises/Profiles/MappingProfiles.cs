using Application.Features.WorkoutTemplateDayExercises.Commands.Create;
using Application.Features.WorkoutTemplateDayExercises.Commands.Delete;
using Application.Features.WorkoutTemplateDayExercises.Commands.Update;
using Application.Features.WorkoutTemplateDayExercises.Queries.GetById;
using Application.Features.WorkoutTemplateDayExercises.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.WorkoutTemplateDayExercises.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<WorkoutTemplateDayExercise, CreateWorkoutTemplateDayExerciseCommand>().ReverseMap();
        CreateMap<WorkoutTemplateDayExercise, CreatedWorkoutTemplateDayExerciseResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDayExercise, UpdateWorkoutTemplateDayExerciseCommand>().ReverseMap();
        CreateMap<WorkoutTemplateDayExercise, UpdatedWorkoutTemplateDayExerciseResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDayExercise, DeleteWorkoutTemplateDayExerciseCommand>().ReverseMap();
        CreateMap<WorkoutTemplateDayExercise, DeletedWorkoutTemplateDayExerciseResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDayExercise, GetByIdWorkoutTemplateDayExerciseResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDayExercise, GetListWorkoutTemplateDayExerciseListItemDto>().ReverseMap();
        CreateMap<IPaginate<WorkoutTemplateDayExercise>, GetListResponse<GetListWorkoutTemplateDayExerciseListItemDto>>().ReverseMap();
    }
}
