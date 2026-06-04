
using ClashDetectionServer.Models;
using ClashDetectionServer.Contracts.Responses;
namespace ClashDetectionServer.Interfaces;

public interface IGlobalViolationRule
{
    bool RequiresNearbyBuildings => false;
    double? SearchRadius => null;
    IEnumerable<ViolationDto> Evaluate(SitePlan sitePlan, Building Building, IReadOnlyList<Building> NearBybuildings);
}