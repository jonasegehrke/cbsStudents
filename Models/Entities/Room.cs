using System.ComponentModel.DataAnnotations;

namespace cbsStudents.Models.Entities;

public class Room
{
    public int RoomId { get; set; }
    public string? Location { get; set; }
    
    public List<Event>? Events { get; set; }


}