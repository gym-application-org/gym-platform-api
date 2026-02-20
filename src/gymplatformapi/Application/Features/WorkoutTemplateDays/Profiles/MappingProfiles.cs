using Application.Features.WorkoutTemplateDays.Commands.Create;
using Application.Features.WorkoutTemplateDays.Commands.Delete;
using Application.Features.WorkoutTemplateDays.Commands.Update;
using Application.Features.WorkoutTemplateDays.Queries.GetById;
using Application.Features.WorkoutTemplateDays.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.WorkoutTemplateDays.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<WorkoutTemplateDay, CreateWorkoutTemplateDayCommand>().ReverseMap();
        CreateMap<WorkoutTemplateDay, CreatedWorkoutTemplateDayResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDay, UpdateWorkoutTemplateDayCommand>().ReverseMap();
        CreateMap<WorkoutTemplateDay, UpdatedWorkoutTemplateDayResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDay, DeleteWorkoutTemplateDayCommand>().ReverseMap();
        CreateMap<WorkoutTemplateDay, DeletedWorkoutTemplateDayResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDay, GetByIdWorkoutTemplateDayResponse>().ReverseMap();
        CreateMap<WorkoutTemplateDay, GetListWorkoutTemplateDayListItemDto>().ReverseMap();
        CreateMap<IPaginate<WorkoutTemplateDay>, GetListResponse<GetListWorkoutTemplateDayListItemDto>>().ReverseMap();
    }
}
