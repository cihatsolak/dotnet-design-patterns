namespace MembershipSystem.Observer.EventHandlers
{
    public class CreatedUserWriteConsoleEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<CreatedUserWriteConsoleEventHandler> _logger;

        public CreatedUserWriteConsoleEventHandler(ILogger<CreatedUserWriteConsoleEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("user created : id: {@userId}", notification.AppUser.Id);

            return Task.CompletedTask;
        }
    }
}
