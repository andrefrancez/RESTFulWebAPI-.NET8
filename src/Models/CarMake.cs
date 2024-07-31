namespace VehiclesAPI.Models;

public class CarMake
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 
    public string ImageUrl { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }
}
