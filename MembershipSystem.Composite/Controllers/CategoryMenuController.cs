using MembershipSystem.Composite.Composite;
using MembershipSystem.Composite.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MembershipSystem.Composite.Controllers
{
    [Authorize]
    public class CategoryMenuController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public CategoryMenuController(AppIdentityDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // category => bookcomposite;
            // book => bookcomponent
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var categories = await _context.Categories.Include(x => x.Books).Where(x => x.UserId == userId).OrderBy(x => x.Id).ToListAsync();
            var menu = GetMenus(categories, new Category { Name = "TopCategory", Id = 0 }, new BookComposite(0, "TopMenu"));

            ViewBag.Menu = menu;
            ViewBag.SelectList = menu.Components.SelectMany(x => ((BookComposite)x).GetSelectListItems(""));
            return View();
        }

        public BookComposite GetMenus(List<Category> categories, Category topCategory, BookComposite topBookComposite, BookComposite lastBookComposite = null)
        {
            var categoriesByReferance = categories.Where(x => x.ReferenceId == topCategory.Id);

            foreach (var categoryByReference in categoriesByReferance)
            {
                BookComposite bookComposite = new(categoryByReference.Id, categoryByReference.Name);

                foreach (var book in categoryByReference.Books)
                {
                    bookComposite.Add(new BookComponent(book.Id, book.Name));
                }

                if (lastBookComposite is not null)
                {
                    lastBookComposite.Add(bookComposite);
                }
                else
                {
                    topBookComposite.Add(bookComposite);
                }

                GetMenus(categories, categoryByReference, topBookComposite, bookComposite);
            }

            return topBookComposite;
        }
    }
}