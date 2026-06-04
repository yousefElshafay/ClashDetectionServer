using System.ComponentModel.DataAnnotations;
using ClashDetectionServer.Models;

namespace ClashDetectionServer.Contracts.Requests;



/// <summary>
/// Site boundary dimensions.
/// </summary>
public class SitePlanRequest
{
    [Required(ErrorMessage = "Width is required.")]
    [Range(0.0000001, double.MaxValue, ErrorMessage = "Width be present and must be greater than 0.")]
    public double Width { get; set; }

    [Required(ErrorMessage = "Length is required.")]
    [Range(0.0000001, double.MaxValue, ErrorMessage = "Length  be present and must be greater than 0.")]
    public double Length { get; set; }
}
