using Dvik.Core;
using Dvik.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dvik.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly IData<Course> courseData;

        public Course Course { get; set; }

        public DeleteModel(IData<Course> courseData)
        {
            this.courseData = courseData;
        }
        public async Task<IActionResult> OnGet(int courseId)
        {
            this.Course = await this.courseData.SearchByIdAsync(courseId);
            return this.Course == null 
                ? this.RedirectToPage("./NotFound") 
                : (IActionResult)this.Page();
        }

        public async Task<IActionResult> OnPost(int courseId)
        {
            var course = await this.courseData.DeleteAsync(courseId);

            if(course == null)
            {
                return this.RedirectToPage("./List");
            }
            await this.courseData.CommitAsync();
            this.TempData["Message"] = $"Курс {course.Name} удалён";
            return this.RedirectToPage("./List");
        }
    }
}