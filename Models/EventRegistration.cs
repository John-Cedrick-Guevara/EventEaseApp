using System.ComponentModel.DataAnnotations;

namespace EventEaseApp.Models;

public class EventRegistration
{
    public int Id { get; set; }
    public int EventId { get; set; }
    
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string FullName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; } = string.Empty;
    
    [Range(1, 10, ErrorMessage = "Number of attendees must be between 1 and 10")]
    public int NumberOfAttendees { get; set; } = 1;
    
    [StringLength(500, ErrorMessage = "Special requests cannot exceed 500 characters")]
    public string? SpecialRequests { get; set; }
    
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}
