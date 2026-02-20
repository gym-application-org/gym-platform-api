using Core.Application.Responses;

namespace Application.Features.ProgressEntries.Commands.Delete;

public class DeletedProgressEntryResponse : IResponse
{
    public int Id { get; set; }
}
