
using ClashDetectionServer.Models;
using ClashDetectionServer.Contracts.Responses;
namespace ClashDetectionServer.Interfaces;

public interface ISpatialIndexService
{
    void Build(IReadOnlyList<Building> buildings);
    IReadOnlyList<Building> FindNearby(Building building, double radius);
}