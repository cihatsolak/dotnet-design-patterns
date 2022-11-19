namespace MembershipSystem.Template.Models
{
    public class AppUser : IdentityUser
    {
        public string PictureUrl { get; set; }
        public string Description { get; set; }
    }
}
