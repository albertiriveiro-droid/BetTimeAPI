namespace BetTime.Models;



public class PlayerUpdateDTO
{
    public string? Name { get; set; }
    public int? TeamId { get; set; }
    public int? ShirtNumber { get; set; }
    public PlayerPosition? Position { get; set; }
    public bool? IsActive { get; set; }
}