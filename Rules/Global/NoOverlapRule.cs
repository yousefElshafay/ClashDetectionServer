using ClashDetectionServer.Contracts.Responses;
using ClashDetectionServer.Interfaces;
using ClashDetectionServer.Models;
using ClashDetectionServer.Shared;
namespace ClashDetectionServer.Rules.Global;

public class NoOverlapRule : IGlobalViolationRule
{
    public bool RequiresNearbyBuildings => true;
    public double? SearchRadius => 0;
    public IEnumerable<ViolationDto> Evaluate(SitePlan sitePlan, Building building, IReadOnlyList<Building> nearbyBuildings)
    {
        foreach (var nearby in nearbyBuildings)
        {
            if (building.Id.CompareTo(nearby.Id) >= 0)
            {
                continue;
            }
            if (!BuildingGeometry.Overlaps(building, nearby))
            {
                continue;
            }
            yield return new ViolationDto
            {
                Code = "OverlapViolation",
                BuildingA = building.Name,
                BuildingB = nearby.Name,
                Message = $"Buildings '{building.Name}' and '{nearby.Name}' overlap."
            };
        }
    }
}