using Application.Features.Exercises.Commands.Create;
using Application.Features.Exercises.Commands.Delete;
using Application.Features.Exercises.Commands.Update;
using Application.Features.Exercises.Queries.GetById;
using Application.Features.Exercises.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Exercises.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Exercise, CreateExerciseCommand>().ReverseMap();
        CreateMap<Exercise, CreatedExerciseResponse>().ReverseMap();
        CreateMap<Exercise, UpdateExerciseCommand>().ReverseMap();
        CreateMap<Exercise, UpdatedExerciseResponse>().ReverseMap();
        CreateMap<Exercise, DeleteExerciseCommand>().ReverseMap();
        CreateMap<Exercise, DeletedExerciseResponse>().ReverseMap();
        CreateMap<Exercise, GetByIdExerciseResponse>().ReverseMap();
        CreateMap<Exercise, GetListExerciseListItemDto>().ReverseMap();
        CreateMap<IPaginate<Exercise>, GetListResponse<GetListExerciseListItemDto>>().ReverseMap();
    }
}
