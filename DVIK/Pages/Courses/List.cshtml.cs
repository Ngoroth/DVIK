using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dvik.Core;
using Dvik.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Dvik.Pages.Courses
{
    public class ListModel : PageModel
    {
        private readonly DvikDbContext dbContext;

        public IEnumerable<Course> Courses { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public ListModel(DvikDbContext courseData)
        {
            this.dbContext = courseData;
        }
        public async Task OnGet()
        {
            this.Courses = await this.dbContext.Courses.Where(c => string.IsNullOrEmpty(this.SearchTerm)
            || c.Name == this.SearchTerm).ToArrayAsync();
        }
    }
}