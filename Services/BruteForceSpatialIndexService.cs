using ClashDetectionServer.Models;
using ClashDetectionServer.Contracts.Responses;
using ClashDetectionServer.Interfaces;
using System.Reflection.Emit;
using ClashDetectionServer.Shared;

namespace ClashDetectionServer.Services;

public class BruteForceSpatialIndexService : ISpatialIndexService
{
    private IReadOnlyList<Building> buildings = [];
    public void Build(IReadOnlyList<Building> buildings)
    {
        this.buildings = buildings;
    }
    public IReadOnlyList<Building> FindNearby(Building building, double radius)
    {
        return buildings
            .Where(other => other.Id != building.Id && BuildingGeometry.Distance(building, other) <= radius)
            .ToList();
    }
}