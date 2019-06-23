using Dvik.Core;
using Dvik.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dvik.Pages.Courses
{
    public class EditModel : PageModel
    {
        public EditModel(IData<Course> courseData)
        {
            this.courseData = courseData;
        }

        private readonly IData<Course> courseData;

        [BindProperty]
        public Course Course { get; set; }
        public async Task<IActionResult> OnGet(int? courseId)
        {
            this.Course = courseId.HasValue ? await this.courseData.SearchByIdAsync(courseId.Value) : new Course();

            return null == this.Course 
                ? this.RedirectToPage("./List") 
                : (IActionResult)this.Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.Course = this.Course.Id > 0 
                ? this.courseData.Update(this.Course) 
                : await this.courseData.AddAsync(this.Course);

            await this.courseData.CommitAsync();
            this.TempData["Message"] = "Курс сохранен.";
            return this.RedirectToPage("./Detail", new { courseId = this.Course.Id });
        }
    }
}