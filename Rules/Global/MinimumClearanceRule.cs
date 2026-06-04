using ClashDetectionServer.Contracts.Responses;
using ClashDetectionServer.Models;
using ClashDetectionServer.Interfaces;
using ClashDetectionServer.Shared;
namespace ClashDetectionServer.Rules.Global;

public class MinimumClearanceRule : IGlobalViolationRule
{
    public bool RequiresNearbyBuildings => true;
    private const double MinimumDistance = 10;

    public double? SearchRadius => MinimumDistance; // Define a search radius for nearby buildings

    public IEnumerable<ViolationDto> Evaluate(SitePlan sitePlan,Building building, IReadOnlyList<Building> NearBybuildings)
    {
       foreach (var nearby in NearBybuildings)
        {
            if (building.Id.CompareTo(nearby.Id) >= 0)
                continue; // Skip self

            var distance = BuildingGeometry.Distance(building, nearby);
            if (distance < MinimumDistance)
            {
                yield return new ViolationDto
                {
                    Code = "MinimumClearanceViolation",
                    BuildingA = building.Name,
                    BuildingB = nearby.Name,
                    Message = $"Buildings '{building.Name}' and '{nearby.Name}' are too close. Minimum clearance of {MinimumDistance} units is required.",
                };
            }
        }
    }
}
