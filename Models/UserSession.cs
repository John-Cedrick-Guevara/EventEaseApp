namespace EventEaseApp.Models;

/// <summary>
/// Represents the current user's session data and preferences
/// </summary>
public class UserSession
{
    public string SessionId { get; set; } = Guid.NewGuid().ToString();
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime SessionStartTime { get; set; } = DateTime.Now;
    public DateTime LastActivityTime { get; set; } = DateTime.Now;
    public List<int> ViewedEvents { get; set; } = new();
    public List<int> RegisteredEvents { get; set; } = new();
    public int PageViews { get; set; } = 0;
    
    /// <summary>
    /// Updates the last activity timestamp
    /// </summary>
    public void UpdateActivity()
    {
        LastActivityTime = DateTime.Now;
        PageViews++;
    }
    
    /// <summary>
    /// Gets the session duration
    /// </summary>
    public TimeSpan SessionDuration => DateTime.Now - SessionStartTime;
    
    /// <summary>
    /// Checks if user has registered for a specific event
    /// </summary>
    public bool IsRegisteredForEvent(int eventId) => RegisteredEvents.Contains(eventId);
}
