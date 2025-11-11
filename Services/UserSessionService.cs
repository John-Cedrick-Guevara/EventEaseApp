using EventEaseApp.Models;

namespace EventEaseApp.Services;

/// <summary>
/// Scoped service to manage user session state throughout the application
/// Each user gets their own instance that persists during their session
/// </summary>
public class UserSessionService
{
    private readonly UserSession _session;
    
    // Event to notify when session changes
    public event Action? OnSessionChanged;

    public UserSessionService()
    {
        _session = new UserSession();
    }

    /// <summary>
    /// Gets the current user session
    /// </summary>
    public UserSession CurrentSession => _session;

    /// <summary>
    /// Sets the user information in the session
    /// </summary>
    public void SetUserInfo(string userName, string email)
    {
        _session.UserName = userName;
        _session.Email = email;
        _session.UpdateActivity();
        NotifyStateChanged();
    }

    /// <summary>
    /// Tracks when a user views an event
    /// </summary>
    public void TrackEventView(int eventId)
    {
        if (!_session.ViewedEvents.Contains(eventId))
        {
            _session.ViewedEvents.Add(eventId);
        }
        _session.UpdateActivity();
        NotifyStateChanged();
    }

    /// <summary>
    /// Records when a user registers for an event
    /// </summary>
    public void RegisterForEvent(int eventId)
    {
        if (!_session.RegisteredEvents.Contains(eventId))
        {
            _session.RegisteredEvents.Add(eventId);
        }
        _session.UpdateActivity();
        NotifyStateChanged();
    }

    /// <summary>
    /// Tracks page navigation
    /// </summary>
    public void TrackPageView()
    {
        _session.UpdateActivity();
        NotifyStateChanged();
    }

    /// <summary>
    /// Checks if the user has viewed a specific event
    /// </summary>
    public bool HasViewedEvent(int eventId)
    {
        return _session.ViewedEvents.Contains(eventId);
    }

    /// <summary>
    /// Checks if the user is registered for an event
    /// </summary>
    public bool IsRegisteredForEvent(int eventId)
    {
        return _session.IsRegisteredForEvent(eventId);
    }

    /// <summary>
    /// Gets session statistics
    /// </summary>
    public string GetSessionStats()
    {
        var duration = _session.SessionDuration;
        return $"Session: {duration.Minutes}m {duration.Seconds}s | Views: {_session.PageViews} | Events Viewed: {_session.ViewedEvents.Count} | Registered: {_session.RegisteredEvents.Count}";
    }

    /// <summary>
    /// Notifies subscribers that the session state has changed
    /// </summary>
    private void NotifyStateChanged() => OnSessionChanged?.Invoke();
}
