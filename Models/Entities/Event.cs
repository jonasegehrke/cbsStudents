using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace cbsStudents.Models.Entities;

public class Event
{
    public int EventId { get; set; }
    public string? Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public string? UserId { get; set; }
    public IdentityUser? User { get; set; }

}