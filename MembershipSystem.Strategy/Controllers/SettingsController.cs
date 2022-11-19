namespace MembershipSystem.Strategy.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Setting setting = new();
            if (User.Claims.Any(x => x.Type == Setting.ClaimDatabaseType))
            {
                setting.DatabaseType = (EDatabaseType)int.Parse(User.Claims.First(x => x.Type == Setting.ClaimDatabaseType).Value);
            }
            else
            {
                setting.DatabaseType = setting.GetDefaultDatabaseType;
            }

            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var newClaim = new Claim(Setting.ClaimDatabaseType, databaseType.ToString());

            var claims = await _userManager.GetClaimsAsync(user);

            var hasDatabaseTypeClaim = claims.FirstOrDefault(x => x.Type == Setting.ClaimDatabaseType);
            if (hasDatabaseTypeClaim is not null)
            {
                await _userManager.ReplaceClaimAsync(user, hasDatabaseTypeClaim, newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(user, newClaim);
            }


            await _signInManager.SignOutAsync();

            var authenticateResult = await HttpContext.AuthenticateAsync();
            await _signInManager.SignInAsync(user, authenticateResult.Properties);

            return RedirectToAction(nameof(Index));
        }
    }
}
