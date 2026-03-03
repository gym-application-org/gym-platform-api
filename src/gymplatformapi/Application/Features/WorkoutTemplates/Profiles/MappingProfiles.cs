using Application.Features.WorkoutTemplates.Commands.Create;
using Application.Features.WorkoutTemplates.Commands.Delete;
using Application.Features.WorkoutTemplates.Commands.Update;
using Application.Features.WorkoutTemplates.Queries.GetById;
using Application.Features.WorkoutTemplates.Queries.GetById.Dtos;
using Application.Features.WorkoutTemplates.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using OtpNet;

namespace Application.Features.WorkoutTemplates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<WorkoutTemplate, CreateWorkoutTemplateCommand>().ReverseMap();
        CreateMap<WorkoutTemplate, CreatedWorkoutTemplateResponse>().ReverseMap();
        CreateMap<WorkoutTemplate, UpdateWorkoutTemplateCommand>().ReverseMap();
        CreateMap<WorkoutTemplate, UpdatedWorkoutTemplateResponse>().ReverseMap();
        CreateMap<WorkoutTemplate, DeleteWorkoutTemplateCommand>().ReverseMap();
        CreateMap<WorkoutTemplate, DeletedWorkoutTemplateResponse>().ReverseMap();
        CreateMap<WorkoutTemplate, GetListWorkoutTemplateListItemDto>().ReverseMap();
        CreateMap<IPaginate<WorkoutTemplate>, GetListResponse<GetListWorkoutTemplateListItemDto>>().ReverseMap();
        CreateMap<WorkoutTemplate, GetByIdWorkoutTemplateResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDay, GetByIdWorkoutTemplateDayDto>();
        CreateMap<WorkoutTemplateDayExercise, GetByIdWorkoutTemplateDayExerciseDto>();
        CreateMap<WorkoutTemplate, GetListWorkoutTemplateListItemDto>()
            .ForMember(dest => dest.DayCount, opt => opt.MapFrom(src => src.Days.Count))
            .ReverseMap();
    }
}
