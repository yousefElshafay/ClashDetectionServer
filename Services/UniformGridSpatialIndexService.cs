using ClashDetectionServer.Models;
using ClashDetectionServer.Contracts.Responses;
using ClashDetectionServer.Interfaces;
using System.Reflection.Emit;
using ClashDetectionServer.Shared;

namespace ClashDetectionServer.Services;

public class UniformGridSpatialIndexService : ISpatialIndexService
{
    //create grid
    //create index per building type for quick grid lookup

    public void Build(IReadOnlyList<Building> buildings)
    {
        /* used to build a spatial index for the buildings, but for simplicity and time constraints, 
        we will not implement it here. In a real implementation, 
        we would create a grid and assign buildings to grid cells based on their location and size.
        */
    }
    public IReadOnlyList<Building> FindNearby(Building building, double radius)
    {
        /* used to find nearby buildings within a specified radius, but for simplicity and time constraints, 
        we will not implement it here. In a real implementation, 
        we would query the grid to find nearby buildings based on their location and size within the grid.
        */
        return [];
    }
}