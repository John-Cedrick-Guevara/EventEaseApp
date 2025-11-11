using EventEaseApp.Models;

namespace EventEaseApp.Services;

public class EventService
{
    private readonly List<Event> _events;
    private readonly List<EventRegistration> _registrations;
    private readonly List<AttendeeInfo> _attendees;
    private int _nextRegistrationId = 1;
    private int _nextAttendeeId = 1;

    public EventService()
    {
        // Initialize with mock data
        _events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Name = "Tech Conference 2025",
                Date = new DateTime(2025, 12, 15, 9, 0, 0),
                Location = "Seattle Convention Center, WA",
                Description = "Join us for the biggest tech conference of the year featuring industry leaders and innovative technologies. Explore cutting-edge developments in AI, cloud computing, and software engineering. Network with professionals and discover the future of technology."
            },
            new Event
            {
                Id = 2,
                Name = "Community Picnic",
                Date = new DateTime(2025, 11, 25, 12, 0, 0),
                Location = "Central Park, New York",
                Description = "A fun family-friendly gathering with food, games, and entertainment for all ages. Bring your family and friends for a day of outdoor activities, delicious food, and community bonding. Live music and kids' activities included."
            },
            new Event
            {
                Id = 3,
                Name = "Charity Gala Evening",
                Date = new DateTime(2025, 12, 1, 18, 30, 0),
                Location = "Grand Ballroom, Chicago",
                Description = "An elegant evening to support local charities with dinner, live music, and auctions. Dress to impress and help make a difference in our community. All proceeds go to supporting education and healthcare initiatives."
            },
            new Event
            {
                Id = 4,
                Name = "Developer Workshop",
                Date = new DateTime(2025, 11, 20, 14, 0, 0),
                Location = "Tech Hub, San Francisco",
                Description = "Hands-on workshop covering the latest in .NET development and Blazor applications. Learn best practices, build real-world projects, and enhance your skills. Suitable for intermediate to advanced developers."
            },
            new Event
            {
                Id = 5,
                Name = "Music Festival 2025",
                Date = new DateTime(2025, 12, 10, 16, 0, 0),
                Location = "Austin Music Arena, TX",
                Description = "Experience live performances from top artists across multiple genres. Three stages, food trucks, and an unforgettable atmosphere. Get ready for the music event of the year!"
            }
        };

        _registrations = new List<EventRegistration>();
        _attendees = new List<AttendeeInfo>();
    }

    // Event methods
    public List<Event> GetAllEvents()
    {
        return _events.OrderBy(e => e.Date).ToList();
    }

    public Event? GetEventById(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public List<Event> GetUpcomingEvents()
    {
        return _events.Where(e => e.Date >= DateTime.Now).OrderBy(e => e.Date).ToList();
    }

    // Registration methods
    public bool RegisterForEvent(EventRegistration registration)
    {
        var eventExists = _events.Any(e => e.Id == registration.EventId);
        if (!eventExists)
            return false;

        registration.Id = _nextRegistrationId++;
        registration.RegistrationDate = DateTime.Now;
        _registrations.Add(registration);
        return true;
    }

    public List<EventRegistration> GetRegistrationsByEvent(int eventId)
    {
        return _registrations.Where(r => r.EventId == eventId).ToList();
    }

    public int GetRegistrationCount(int eventId)
    {
        return _registrations.Where(r => r.EventId == eventId).Sum(r => r.NumberOfAttendees);
    }

    // Attendance tracking methods
    public bool AddAttendee(AttendeeInfo attendee)
    {
        var eventExists = _events.Any(e => e.Id == attendee.EventId);
        if (!eventExists)
            return false;

        attendee.Id = _nextAttendeeId++;
        attendee.RegistrationDate = DateTime.Now;
        _attendees.Add(attendee);
        return true;
    }

    public List<AttendeeInfo> GetAttendeesByEvent(int eventId)
    {
        return _attendees
            .Where(a => a.EventId == eventId)
            .OrderByDescending(a => a.RegistrationDate)
            .ToList();
    }

    public int GetTotalAttendeesCount(int eventId)
    {
        return _attendees
            .Where(a => a.EventId == eventId)
            .Sum(a => a.NumberOfAttendees);
    }

    public int GetCheckedInCount(int eventId)
    {
        return _attendees
            .Where(a => a.EventId == eventId && a.CheckedIn)
            .Sum(a => a.NumberOfAttendees);
    }

    public bool CheckInAttendee(int attendeeId)
    {
        var attendee = _attendees.FirstOrDefault(a => a.Id == attendeeId);
        if (attendee == null)
            return false;

        attendee.CheckedIn = true;
        attendee.CheckInTime = DateTime.Now;
        return true;
    }

    public AttendeeInfo? GetAttendeeByEmail(int eventId, string email)
    {
        return _attendees.FirstOrDefault(a => 
            a.EventId == eventId && 
            a.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }
}
