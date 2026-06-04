using System.ComponentModel.DataAnnotations;
using ClashDetectionServer.Models;

namespace ClashDetectionServer.Contracts.Requests;

public class BuildingRequest
{

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Type is required")]
    [RegularExpression("^(School|Nightclub|Stadium|ResidentialBuilding|Office)$",
    ErrorMessage = "Type must be one of: School, Nightclub, Stadium, ResidentialBuilding, Office.")]
    public string Type { get; set; }

    [Required(ErrorMessage = "Width is required.")]
    [Range(0.0000001, double.MaxValue, ErrorMessage = "Width must be greater than 0.")]
    public double Width { get; set; }

    [Required(ErrorMessage = "Length is required.")]
    [Range(0.0000001, double.MaxValue, ErrorMessage = "Length must be greater than 0.")]
    public double Length { get; set; }

    [Required(ErrorMessage = "X is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "X must be a non-negative number.")]
    public required double X { get; set; }

    [Required(ErrorMessage = "Y is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Y must be a non-negative number.")]
    public required double Y { get; set; }
}
