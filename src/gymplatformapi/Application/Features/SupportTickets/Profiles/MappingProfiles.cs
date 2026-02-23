using Application.Features.SupportTickets.Commands.Create;
using Application.Features.SupportTickets.Commands.Delete;
using Application.Features.SupportTickets.Commands.DeleteAdmin;
using Application.Features.SupportTickets.Commands.Update;
using Application.Features.SupportTickets.Commands.UpdateAdmin;
using Application.Features.SupportTickets.Queries.GetAdminById;
using Application.Features.SupportTickets.Queries.GetAdminList;
using Application.Features.SupportTickets.Queries.GetById;
using Application.Features.SupportTickets.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.SupportTickets.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SupportTicket, CreateSupportTicketCommand>().ReverseMap();
        CreateMap<SupportTicket, CreatedSupportTicketResponse>().ReverseMap();
        CreateMap<SupportTicket, UpdateSupportTicketCommand>().ReverseMap();
        CreateMap<SupportTicket, UpdatedSupportTicketResponse>().ReverseMap();
        CreateMap<SupportTicket, UpdateAdminSupportTicketCommand>().ReverseMap();
        CreateMap<SupportTicket, UpdatedAdminSupportTicketResponse>().ReverseMap();
        CreateMap<SupportTicket, DeleteAdminSupportTicketCommand>().ReverseMap();
        CreateMap<SupportTicket, DeletedAdminSupportTicketResponse>().ReverseMap();
        CreateMap<SupportTicket, DeleteSupportTicketCommand>().ReverseMap();
        CreateMap<SupportTicket, DeletedSupportTicketResponse>().ReverseMap();
        CreateMap<SupportTicket, GetByIdSupportTicketResponse>().ReverseMap();
        CreateMap<SupportTicket, GetAdminByIdSupportTicketResponse>().ReverseMap();
        CreateMap<SupportTicket, GetListSupportTicketListItemDto>().ReverseMap();
        CreateMap<IPaginate<SupportTicket>, GetListResponse<GetListSupportTicketListItemDto>>().ReverseMap();
        CreateMap<SupportTicket, GetAdminListSupportTicketListItemDto>().ReverseMap();
        CreateMap<IPaginate<SupportTicket>, GetListResponse<GetAdminListSupportTicketListItemDto>>().ReverseMap();
    }
}
