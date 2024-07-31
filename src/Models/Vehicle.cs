namespace VehiclesAPI.Models;

public class Vehicle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime Year { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public int CarMakeId { get; set; }
    public CarMake CarMake { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
    