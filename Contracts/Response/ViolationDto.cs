namespace ClashDetectionServer.Contracts.Responses;

/// <summary>
/// Standard clash output model.
/// </summary>
public class ViolationDto
{
    public required string Code { get; set; }

    public required string Message { get; set; }

    public required string BuildingA { get; set; }

    public string? BuildingB { get; set; }

    public double? ActualDistance { get; set; }

    public double? RequiredDistance { get; set; }
}
