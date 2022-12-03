namespace MembershipSystem.Composite.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ReferenceId { get; set; }
        public string UserId { get; set; }
        public List<Book> Books { get; set; }
    }
}
