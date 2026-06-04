namespace ClashDetectionServer.Models;

public class Building
{
    public Building(string name, BuildingType type, double x, double y, double width, double length)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        X = x;
        Y = y;
        Width = width;
        Length = length;
    }
    public Guid Id { get; }
    public string Name { get; }

    public BuildingType Type { get; }


    public double Width { get; }
    public double Length { get; }
    public double X { get; }
    public double Y { get; }

}
