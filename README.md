# EventEase App - Social Event Management

A modern Blazor WebAssembly application for managing and registering for social events, built with .NET 9.0 following best practices.

## 🎯 Features Implemented

### 1. **Event Card Component** (`Components/EventCard.razor`)

- Reusable card component displaying event information
- Fields: Event Name, Date, Time, Location, Description (truncated)
- Clickable cards with hover effects
- "View Details" button for navigation
- Scoped CSS styling with gradient headers

### 2. **Complete Navigation Flow**

Three interconnected pages with seamless routing:

#### **Events List** (`/events`)

- Displays all upcoming events in a responsive grid
- Session tracker displaying user activity statistics
- Click any card to view event details
- Auto-sorted by date

#### **Event Details** (`/events/{id}`)

- Route parameters for dynamic event loading
- Full event information display
- Registration count and countdown timer
- **Attendee List** showing all registered participants
- "Register for This Event" call-to-action button
- Back navigation to events list

#### **Registration Page** (`/events/{id}/register`)

- Complete registration form with validation
- Two-way data binding on all input fields
- Success confirmation screen after submission
- Automatic attendee tracking upon registration

### 3. **Two-Way Data Binding Implementation**

The registration form demonstrates Blazor's two-way data binding using `@bind-Value`:

```razor
<InputText @bind-Value="registration.FullName" />
<InputText @bind-Value="registration.Email" />
<InputText @bind-Value="registration.Phone" />
<InputNumber @bind-Value="registration.NumberOfAttendees" />
<InputTextArea @bind-Value="registration.SpecialRequests" />
```

### 4. **User Session Tracking** 🆕

Complete session management to track user activity:

- **UserSessionService** (Scoped service)
  - Unique session ID per user
  - Tracks page views and navigation
  - Records viewed events
  - Monitors registered events
  - Session duration tracking
  - Real-time stats updates

**Session Features:**

- Automatic page view tracking
- Event view history
- Registration history
- Session statistics display
- User identification after registration

### 5. **Attendance Tracking System** 🆕

Comprehensive attendance monitoring for events:

- **AttendeeList Component** (`Components/AttendeeList.razor`)
  - Displays all registered participants
  - Shows attendee avatars with initials
  - Registration date and guest count
  - Check-in status tracking
  - Total attendee count
  - Checked-in count display

**Attendee Information Tracked:**

- Full name and contact details
- Number of guests
- Registration date and time
- Session ID linkage
- Check-in status and timestamp
- Email and phone for contact

### 6. **State Management with Dependency Injection**

Following best practices with two complementary services:

- **EventService** (`Services/EventService.cs`) - Singleton

  - Centralized event and attendance data management
  - Registered as Singleton in `Program.cs`
  - Methods for CRUD operations on events, registrations, and attendees
  - Attendance tracking and check-in functionality

- **UserSessionService** (`Services/UserSessionService.cs`) - Scoped
  - User-specific session state management
  - Each user gets their own service instance
  - Event notification system for state changes
  - Automatic activity tracking

### 7. **Data Models**

- **Event Model** (`Models/Event.cs`)
  - Id, Name, Date, Location, Description
- **EventRegistration Model** (`Models/EventRegistration.cs`)

  - Data annotations for validation
  - Required fields with custom error messages
  - Email and phone validation
  - Range validation for attendee count

- **AttendeeInfo Model** (`Models/AttendeeInfo.cs`) 🆕

  - Comprehensive participant tracking
  - Check-in status and timestamps
  - Session ID linkage
  - Computed initials for avatars

- **UserSession Model** (`Models/UserSession.cs`) 🆕
  - Session identification and tracking
  - Activity monitoring
  - Event history
  - Session duration calculation

## 🏗️ Architecture & Best Practices

### Dependency Injection

```csharp
// Program.cs
builder.Services.AddSingleton<EventService>();  // Shared across all users
builder.Services.AddScoped<UserSessionService>(); // Per-user instance
```

### Service Communication Pattern

```csharp
// Session state updates
SessionService.OnSessionChanged += OnSessionChanged;

// Track user actions
SessionService.TrackEventView(eventId);
SessionService.RegisterForEvent(eventId);
```

### Route Parameters

```razor
@page "/events/{EventId:int}"
[Parameter]
public int EventId { get; set; }
```

### Navigation Manager

```csharp
@inject NavigationManager Navigation
Navigation.NavigateTo($"/events/{EventId}");
```

### Form Validation

```razor
<EditForm Model="@registration" OnValidSubmit="HandleValidSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />
    <ValidationMessage For="@(() => registration.FullName)" />
</EditForm>
```

### Scoped CSS

Each component has its own `.razor.css` file for isolated styling, preventing style conflicts.

## 📁 Project Structure

```
EventEaseApp/
├── Components/
│   ├── EventCard.razor              # Reusable event card component
│   ├── EventCard.razor.css          # Scoped component styles
│   ├── AttendeeList.razor           # 🆕 Participant list component
│   ├── AttendeeList.razor.css
│   ├── SessionTracker.razor         # 🆕 User session tracking display
│   └── SessionTracker.razor.css
├── Models/
│   ├── Event.cs                     # Event data model
│   ├── EventRegistration.cs         # Registration form model
│   ├── AttendeeInfo.cs              # 🆕 Attendee tracking model
│   └── UserSession.cs               # 🆕 User session model
├── Pages/
│   ├── Events.razor                 # Events list page with session tracker
│   ├── Events.razor.css
│   ├── EventDetails.razor           # Event details with attendee list
│   ├── EventDetails.razor.css
│   ├── RegisterForEvent.razor       # Registration form page
│   └── RegisterForEvent.razor.css
├── Services/
│   ├── EventService.cs              # Event & attendance management (Singleton)
│   └── UserSessionService.cs        # 🆕 User session tracking (Scoped)
├── Layout/
│   └── NavMenu.razor                # Navigation menu (updated)
├── _Imports.razor                   # Global using directives
└── Program.cs                       # Service registration
```

## 🚀 Running the Application

```powershell
cd "d:\2025 code shits lock in - Copy\microsoft projects\EventEaseApp"
dotnet build
dotnet run
```

Navigate to: **http://localhost:5210**

## 🎨 UI/UX Features

- **Gradient Backgrounds**: Modern purple gradient theme
- **Hover Effects**: Cards lift on hover with shadow effects
- **Session Tracker**: Real-time display of user activity 🆕
- **Attendee Avatars**: Initial-based circular avatars 🆕
- **Check-in Badges**: Visual indicators for checked-in attendees 🆕
- **Responsive Design**: Grid layout adapts to screen sizes
- **Animations**: Scale-in effect on success confirmation
- **Clear CTAs**: Prominent action buttons throughout
- **Breadcrumb Navigation**: Easy back navigation on all pages

## 📝 Navigation Flow

1. **Events List** → Click card or "View Details" button
2. **Event Details** → View full information, click "Register"
3. **Registration Form** → Fill form with validation
4. **Success Screen** → Confirmation with navigation options

## 🔒 Validation Rules

- **Full Name**: Required, 2-100 characters
- **Email**: Required, valid email format
- **Phone**: Required, valid phone format
- **Attendees**: Required, 1-10 range
- **Special Requests**: Optional, max 500 characters

## 🎯 Two-Way Data Binding Example

The registration form showcases real-time two-way binding:

```razor
@code {
    private EventRegistration registration = new();

    // As user types, registration object updates immediately
    // When form submits, object contains all values
    private void HandleValidSubmit()
    {
        // Register and create attendee record
        EventService.RegisterForEvent(registration);

        var attendee = new AttendeeInfo
        {
            EventId = EventId,
            FullName = registration.FullName,
            Email = registration.Email,
            SessionId = SessionService.CurrentSession.SessionId
        };
        EventService.AddAttendee(attendee);

        // Update session tracking
        SessionService.SetUserInfo(registration.FullName, registration.Email);
        SessionService.RegisterForEvent(EventId);
    }
}
```

## � Session & Attendance Tracking Features

### Session Tracking

- **Unique Session IDs**: Each user gets a unique identifier
- **Activity Monitoring**: Tracks page views, event views, and registrations
- **Real-time Stats**: Live display of session duration and activity
- **Event Notification**: Components can subscribe to session changes

### Attendance Management

- **Participant List**: View all registered attendees
- **Avatar Display**: Initial-based circular avatars for each attendee
- **Guest Count**: Track total number of attendees including guests
- **Check-in System**: Mark attendees as checked in with timestamps
- **Registration History**: See when each person registered
- **Session Linkage**: Connect attendees to their user sessions

## �🔄 Future Enhancement Ideas

- Add event creation/editing functionality
- Implement user authentication
- Add search and filter capabilities
- Create event categories
- Add image uploads for events
- Real-time synchronization with SignalR
- Add calendar integration
- Create event reminder notifications
- Export attendee lists to CSV
- QR code check-in system
- Analytics dashboard for event organizers

## 🛠️ Technologies Used

- **.NET 9.0**
- **Blazor WebAssembly**
- **C# 12**
- **CSS3** with custom properties
- **Component-based architecture**
- **Dependency Injection pattern**
- **Data Annotations validation**

---

Built with ❤️ using Blazor best practices for scalable, maintainable event management.
#   E v e n t E a s e A p p 
 
 
