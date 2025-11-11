namespace EventEaseApp.Models;

/// <summary>
/// Represents an attendee's information for event participation tracking
/// </summary>
public class AttendeeInfo
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int NumberOfAttendees { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string SessionId { get; set; } = string.Empty;
    public bool CheckedIn { get; set; } = false;
    public DateTime? CheckInTime { get; set; }
    
    /// <summary>
    /// Gets the initials for avatar display
    /// </summary>
    public string Initials
    {
        get
        {
            var parts = FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[^1][0]}".ToUpper();
            return parts.Length > 0 ? parts[0][0].ToString().ToUpper() : "?";
        }
    }
}
