using GistPage.Data;
using GistPage.Models;
using Microsoft.AspNetCore.Identity;

namespace GistPage.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context; 
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public void Initialize()
        {
            if(!_roleManager.RoleExistsAsync(WebsitesRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsitesRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsitesRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "Admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Chizzy",
                    LastName = "Sent"
                },"Sixela27.").Wait();

                var appUser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if(appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebsitesRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }

                var lostOfPages = new List<Page>()
                {
                    new Page()
                    {
                        Title = "About Us",
                        Slug = "about"
                    },
                    new Page()
                    {
                        Title = "Contact Us",
                        Slug = "contact"
                    },
                    new Page()
                    {
                        Title = "Privacy Policy",
                        Slug = "privacy"
                    }
                };

                _context.Pages.AddRange(lostOfPages);
                _context.SaveChanges();


            }
        }
    }
}
