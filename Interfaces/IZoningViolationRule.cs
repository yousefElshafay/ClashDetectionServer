
using ClashDetectionServer.Models;
using ClashDetectionServer.Contracts.Responses;
namespace ClashDetectionServer.Interfaces;

public interface IZoningViolationRule
{
    IReadOnlyList<BuildingType> SourceBuildingTypes { get; }
    IReadOnlyList<BuildingType> TargetBuildingTypes { get; }
    double SearchRadius { get; }
    IEnumerable<ViolationDto> Evaluate(
        SitePlan sitePlan,
        Building building,
        IReadOnlyList<Building> nearbyBuildings);
}