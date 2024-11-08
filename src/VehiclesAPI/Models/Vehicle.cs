﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehiclesAPI.Models;

public class Vehicle
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime Year { get; set; }
    public string Description { get; set; }

    [MaxLength(300)]
    public string ImageUrl { get; set; }
    public int CarMakeId { get; set; }
    [JsonIgnore]
    public CarMake? CarMake { get; set; }
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
}
    