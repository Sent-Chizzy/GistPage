using AspNetCoreHero.ToastNotification.Abstractions;
using GistPage.Data;
using GistPage.Models;
using GistPage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GistPage.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public INotyfService _notification { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SettingController(ApplicationDbContext context, 
                                INotyfService notyfService, 
                                IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _notification = notyfService;
            _webHostEnvironment= webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = _context.Settings!.ToList();
            if(settings.Count > 0)
            {
                var vm = new SettingVM()
                {
                    Id = settings[0].Id,
                    SiteName = settings[0].SiteName,
                    Title = settings[0].Title,
                    ShortDescription = settings[0].ShortDescription,
                    ThumbnailUrl = settings[0].ThumbnailUrl,
                    FacebookUrl = settings[0].FacebookUrl,
                    TwitterUrl = settings[0].TwitterUrl,
                };
                return View(vm);
            }
            var setting = new Setting()
            {
                SiteName = "Demo Name"
            };

            await _context.Settings!.AddAsync(setting);
            await _context.SaveChangesAsync();
            var createdsettings = _context.Settings!.ToList();
            var createdvm = new SettingVM()
            {
                Id = createdsettings[0].Id,
                SiteName = createdsettings[0].SiteName,
                Title = createdsettings[0].Title,
                ShortDescription = createdsettings[0].ShortDescription,
                ThumbnailUrl = createdsettings[0].ThumbnailUrl,
                FacebookUrl = createdsettings[0].FacebookUrl,
                TwitterUrl = createdsettings[0].TwitterUrl,
            };
            return View(createdvm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SettingVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var setting = await _context.Settings!.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (setting == null)
            {
                _notification.Error("Problem Encontered");
                return View(vm);
            }
            setting.SiteName = vm.SiteName;
            setting.Title = vm.Title;
            setting.ShortDescription = vm.ShortDescription;
            setting.FacebookUrl = vm.FacebookUrl;
            setting.TwitterUrl = vm.TwitterUrl;

            if (vm.Thumbnail != null)
            {
                setting.ThumbnailUrl = UploadImage(vm.Thumbnail);
            }
            await _context.SaveChangesAsync();
            _notification.Success("Setting Successfully Updated");
            return RedirectToAction("Index", "Setting", new {area = "Admin"});
        }

        private string UploadImage(IFormFile file)
        {
            string uniqueFileName = "";
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "thumbnails");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }


    }
}
