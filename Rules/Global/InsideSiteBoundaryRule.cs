using ClashDetectionServer.Contracts.Responses;
using ClashDetectionServer.Models;
using ClashDetectionServer.Interfaces;
using System;
namespace ClashDetectionServer.Rules.Global;

public class InsideSiteBoundaryRule : IGlobalViolationRule
{
    public IEnumerable<ViolationDto> Evaluate(SitePlan sitePlan, Building building, IReadOnlyList<Building> NearBybuildings)
    {

        var exceedsWidth = building.X + building.Width > sitePlan.Width;
        var exceedsLength = building.Y + building.Length > sitePlan.Length;

        if (exceedsWidth || exceedsLength)
        {
            yield return new ViolationDto
            {
                Code = "OutsideSiteBoundary",
                BuildingA = building.Name,
                Message = $"Building '{building.Name}' is outside the site boundary.",
            };
        }
    }
}
