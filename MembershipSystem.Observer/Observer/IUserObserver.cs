namespace MembershipSystem.Observer.Observer
{
    public interface IUserObserver
    {
        void UserCreated(AppUser appUser);
    }
}
