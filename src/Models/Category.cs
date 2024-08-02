using System.ComponentModel.DataAnnotations;

namespace VehiclesAPI.Models;

public class Category
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }
}