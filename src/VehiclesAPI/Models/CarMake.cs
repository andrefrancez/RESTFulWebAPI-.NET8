using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehiclesAPI.Models;

public class CarMake
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }
    public string Description { get; set; }

    [MaxLength(300)]
    public string ImageUrl { get; set; }

    [JsonIgnore]
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
