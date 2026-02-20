using Application.Features.ProgressEntries.Commands.Create;
using Application.Features.ProgressEntries.Commands.Delete;
using Application.Features.ProgressEntries.Commands.Update;
using Application.Features.ProgressEntries.Queries.GetById;
using Application.Features.ProgressEntries.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgressEntries.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProgressEntry, CreateProgressEntryCommand>().ReverseMap();
        CreateMap<ProgressEntry, CreatedProgressEntryResponse>().ReverseMap();
        CreateMap<ProgressEntry, UpdateProgressEntryCommand>().ReverseMap();
        CreateMap<ProgressEntry, UpdatedProgressEntryResponse>().ReverseMap();
        CreateMap<ProgressEntry, DeleteProgressEntryCommand>().ReverseMap();
        CreateMap<ProgressEntry, DeletedProgressEntryResponse>().ReverseMap();
        CreateMap<ProgressEntry, GetByIdProgressEntryResponse>().ReverseMap();
        CreateMap<ProgressEntry, GetListProgressEntryListItemDto>().ReverseMap();
        CreateMap<IPaginate<ProgressEntry>, GetListResponse<GetListProgressEntryListItemDto>>().ReverseMap();
    }
}
