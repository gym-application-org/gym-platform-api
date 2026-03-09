using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Auth.Queries.VerifyActionToken;

public class VerifyActionTokenQueryResponse : IResponse
{
    public string Status { get; set; }
}
