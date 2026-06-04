using ClashDetectionServer.Models;
namespace ClashDetectionServer.Shared;
internal static class BuildingGeometry
{
    public static bool Overlaps(Building a, Building b)
    {
        var aRight = a.X + a.Width;
        var aTop = a.Y + a.Length;
        var bRight = b.X + b.Width;
        var bTop = b.Y + b.Length;
        return a.X < bRight && aRight > b.X && a.Y < bTop && aTop > b.Y;
    }
    public static double Distance(Building a, Building b)
    {
        var aRight = a.X + a.Width;
        var aTop = a.Y + a.Length;
        var bRight = b.X + b.Width;
        var bTop = b.Y + b.Length;
        var dx = Math.Max(0, Math.Max(a.X - bRight, b.X - aRight));
        var dy = Math.Max(0, Math.Max(a.Y - bTop, b.Y - aTop));
        return Math.Sqrt(dx * dx + dy * dy);
    }
}