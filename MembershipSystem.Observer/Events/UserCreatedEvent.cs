namespace MembershipSystem.Observer.Events
{
    public class UserCreatedEvent : INotification
    {
        public AppUser AppUser { get; set; }
    }
}
