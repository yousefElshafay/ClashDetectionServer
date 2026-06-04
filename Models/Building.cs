namespace ClashDetectionServer.Models;

public class Building
{
    public Building( string name, BuildingType type, double x, double y, double length, double width)
    {
        Id =  Guid.NewGuid();
        Name = name;
        Type = type;
        X = x;
        Y = y;
        Length = length;
        Width = width;
    }   
    public Guid Id { get; }
    public  string Name { get; }

    public BuildingType Type { get; }

   
    public double Width { get;}
    public double Length { get; }
    public double X { get;  }
    public double Y { get; }

}
