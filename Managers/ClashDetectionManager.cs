/* after validation with dto 
incoming casting to buildings 
and group types , rules are applicable to all or rules are
applicable to specific group types or buildings 

the manager calls the list or rules against the buildings 
and group types and returns the list of clashes to the controller
*/

using ClashDetectionServer.Interfaces;
using ClashDetectionServer.Models;
using ClashDetectionServer.Contracts.Responses;
using System.Runtime.InteropServices;
namespace ClashDetectionServer.Managers;


public class ClashDetectionManager(IEnumerable<IGlobalViolationRule> globalRules, IEnumerable<IZoningViolationRule> zoningRules, ISpatialIndexService spatialIndexService) : IClashDetectionManager
{
    private List<ViolationDto> clashes = new List<ViolationDto>();
    // for subset rules
    // we need to filter the buildings based on the types
    // and here comes in the search algo wether brutforce
    // or some spatial search algo to reduce the number of comparisons

    public IReadOnlyList<ViolationDto> Detect(SitePlan sitePlan, IReadOnlyList<Building> buildings)
    {


        spatialIndexService.Build(buildings);

        foreach (var building in buildings)
        {
            foreach (var rule in globalRules)
            {
                var nearbyBuildings = rule.RequiresNearbyBuildings
                    ? spatialIndexService.FindNearby(building, rule.SearchRadius ?? double.MaxValue)
                    : [];
                clashes.AddRange(rule.Evaluate(sitePlan, building, nearbyBuildings));
            }

            foreach (var zoningRule in zoningRules)
            {
                if (!zoningRule.SourceBuildingTypes.Contains(building.Type))
                {
                    continue;
                }
                var nearbyBuildings = spatialIndexService
                    .FindNearby(building, zoningRule.SearchRadius)
                    .Where(other => zoningRule.TargetBuildingTypes.Contains(other.Type))
                    .ToList();
                clashes.AddRange(zoningRule.Evaluate(sitePlan, building, nearbyBuildings));
            }
        }


        return clashes;
        // go through the rules and per rule decide if its applicable to all buildings 
        // or specific group types or specific buildings and if so pass only the relevant subset of buildings 
    }

}