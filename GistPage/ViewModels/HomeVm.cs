using GistPage.Models;
using X.PagedList;

namespace GistPage.ViewModels
{
    public class HomeVm
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? ThumbnailUrl { get; set; }
        public IPagedList<Post>? Posts { get; set; }
    }
}
