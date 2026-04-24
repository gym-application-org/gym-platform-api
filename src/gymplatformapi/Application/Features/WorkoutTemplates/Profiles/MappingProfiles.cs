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

namespace Application.Features.WorkoutTemplates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<WorkoutTemplate, CreateWorkoutTemplateCommand>()
            .ForMember(dest => dest.Days, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<WorkoutTemplate, CreatedWorkoutTemplateResponse>();
        CreateMap<WorkoutTemplate, UpdateWorkoutTemplateCommand>()
            .ForMember(dest => dest.Days, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<WorkoutTemplate, UpdatedWorkoutTemplateResponse>();
        CreateMap<WorkoutTemplate, DeleteWorkoutTemplateCommand>().ReverseMap();
        CreateMap<WorkoutTemplate, DeletedWorkoutTemplateResponse>();
        CreateMap<IPaginate<WorkoutTemplate>, GetListResponse<GetListWorkoutTemplateListItemDto>>();
        CreateMap<WorkoutTemplate, GetByIdWorkoutTemplateResponse>();
        CreateMap<WorkoutTemplateDay, GetByIdWorkoutTemplateDayDto>();
        CreateMap<WorkoutTemplateDayExercise, GetByIdWorkoutTemplateDayExerciseDto>();
        CreateMap<WorkoutTemplate, GetListWorkoutTemplateListItemDto>()
            .ForMember(dest => dest.DayCount, opt => opt.MapFrom(src => src.Days.Count));
    }
}
