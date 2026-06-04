namespace ClashDetectionServer.Contracts.Responses;

/// <summary>
/// Response payload for clash detection.
/// </summary>
public class DetectClashesResponse
{
    public List<ViolationDto> Clashes { get; set; } = [];
}
