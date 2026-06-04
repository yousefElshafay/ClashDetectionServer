using ClashDetectionServer.Contracts.Responses;
using ClashDetectionServer.Interfaces;
using ClashDetectionServer.Models;
using ClashDetectionServer.Shared;
namespace ClashDetectionServer.Rules.Zoning;

public class ResidentialDistanceRule : IZoningViolationRule
{
    private const double MinimumDistance = 150;
    public IReadOnlyList<BuildingType> SourceBuildingTypes => [BuildingType.ResidentialBuilding];
    public IReadOnlyList<BuildingType> TargetBuildingTypes => [BuildingType.Stadium, BuildingType.Nightclub];
    public double SearchRadius => MinimumDistance;
    public IEnumerable<ViolationDto> Evaluate(SitePlan sitePlan, Building building, IReadOnlyList<Building> nearbyBuildings)
    {
        foreach (var nearby in nearbyBuildings)
        {
            var distance = BuildingGeometry.Distance(building, nearby);
            if (distance >= MinimumDistance)
            {
                continue;
            }
            yield return new ViolationDto
            {
                Code = "ResidentialDistanceViolation",
                BuildingA = building.Name,
                BuildingB = nearby.Name,
                ActualDistance = distance,
                RequiredDistance = MinimumDistance,
                Message = $"Residential building '{building.Name}' must be at least {MinimumDistance} units away from {nearby.Type} '{nearby.Name}'."
            };
        }
    }
}