using System.Text;

namespace MembershipSystem.Composite.Composite
{
    public class BookComposite : IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private readonly List<IComponent> _components;

        public BookComposite(int id, string name)
        {
            Id = id;
            Name = name;
            _components = new List<IComponent>();
        }

        public void Add(IComponent component)
        {
            _components.Add(component);
        }

        public void Remove(IComponent component)
        {
            _components.Remove(component);
        }

        public int Count()
        {
            return _components.Sum(x => x.Count());
        }

        public string Display()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"<div class='text-primary my-1'><a href='#' class='menu'>{Name} ({Count()}) </a></div>");

            if (!_components.Any()) return stringBuilder.ToString();

            stringBuilder.Append("<ul class='list-group list-group-flush ms-3'>");

            foreach (var item in _components)
            {
                stringBuilder.Append(item.Display());
            }

            stringBuilder.Append("</ul>");

            return stringBuilder.ToString();
        }
    }
}
