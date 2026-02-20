using Core.Application.Responses;

namespace Application.Features.ProgressEntries.Commands.Create;

public class CreatedProgressEntryResponse : IResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal? WeightKg { get; set; }
    public decimal? BodyFatPercent { get; set; }
    public decimal? MuscleMassKg { get; set; }
    public decimal? ChestCm { get; set; }
    public decimal? WaistCm { get; set; }
    public decimal? HipCm { get; set; }
    public decimal? ArmCm { get; set; }
    public decimal? LegCm { get; set; }
    public string? Note { get; set; }
    public Guid MemberId { get; set; }
}
