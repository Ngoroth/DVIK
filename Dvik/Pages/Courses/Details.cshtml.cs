using Dvik.Core;
using Dvik.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dvik.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly DvikDbContext dbContext;
        public Course Course { get; set; }
        public Trainer Trainer { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailsModel(DvikDbContext courseData)
        {
            this.dbContext = courseData;
        }

        public async Task<IActionResult> OnGetAsync(int courseId)
        {
            this.Course = await this.dbContext.Courses.FindAsync(courseId);
            
            return null == this.Course 
                ? this.RedirectToPage("./NotFound") 
                : (IActionResult)this.Page();
        }
    }
}