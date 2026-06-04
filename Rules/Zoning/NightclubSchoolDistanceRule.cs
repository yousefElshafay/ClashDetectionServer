using ClashDetectionServer.Contracts.Responses;
using ClashDetectionServer.Interfaces;
using ClashDetectionServer.Models;
using ClashDetectionServer.Shared;
namespace ClashDetectionServer.Rules.Zoning;

public class NightclubSchoolDistanceRule : IZoningViolationRule
{
    private const double MinimumDistance = 200;
    public IReadOnlyList<BuildingType> SourceBuildingTypes => [BuildingType.Nightclub];
    public IReadOnlyList<BuildingType> TargetBuildingTypes => [BuildingType.School];
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
                Code = "NightclubSchoolDistanceViolation",
                BuildingA = building.Name,
                BuildingB = nearby.Name,
                ActualDistance = distance,
                RequiredDistance = MinimumDistance,
                Message = $"Nightclub '{building.Name}' must be at least {MinimumDistance} units away from school '{nearby.Name}'."
            };
        }
    }
}