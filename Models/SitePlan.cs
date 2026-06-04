namespace ClashDetectionServer.Models;

public class SitePlan
{

    public SitePlan(double width, double length)
    {
        Width = width;
        Length = length;
    }
    public double Width { get; }

    public double Length { get; }
}
