using System.ComponentModel.DataAnnotations;
using ClashDetectionServer.Models;
using ClashDetectionServer.Contracts.Requests;

namespace ClashDetectionServer.Contracts.Requests;
/// <summary>
/// Request payload for clash detection.
/// </summary>
public class DetectClashesRequest
{
    [Required(ErrorMessage = "Site plan is required")]
    public SitePlanRequest SitePlan { get; set; }

    [Required(ErrorMessage = "Buildings are required")]
    [MinLength(1, ErrorMessage = "At least one building is required.")]
    public List<BuildingRequest> Buildings { get; set; } = [];
}
