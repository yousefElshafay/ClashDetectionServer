using ClashDetectionServer.Models;
using ClashDetectionServer.Contracts.Responses;
namespace ClashDetectionServer.Interfaces;

public interface IClashDetectionManager
{
    IReadOnlyList<ViolationDto> Detect(SitePlan sitePlan, IReadOnlyList<Building> buildings);
}