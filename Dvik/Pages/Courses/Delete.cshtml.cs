using Dvik.Core;
using Dvik.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dvik.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly DvikDbContext dbContext;

        public Course Course { get; set; }

        public DeleteModel(DvikDbContext courseData)
        {
            this.dbContext = courseData;
        }
        public async Task<IActionResult> OnGetAsync(int courseId)
        {
            this.Course = await this.dbContext.Courses.FindAsync(courseId);
            return this.Course == null 
                ? this.RedirectToPage("./NotFound") 
                : (IActionResult)this.Page();
        }

        public async Task<IActionResult> OnPostAsync(int courseId)
        {
            var course = await this.dbContext.Courses.FindAsync(courseId);

            if(course == null)
            {
                return this.RedirectToPage("./List");
            }
            this.dbContext.Remove(course);
            await this.dbContext.SaveChangesAsync();
            this.TempData["Message"] = $"Курс {course.Name} удалён";
            return this.RedirectToPage("./List");
        }
    }
}
