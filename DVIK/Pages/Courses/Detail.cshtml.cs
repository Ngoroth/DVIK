using Dvik.Core;
using Dvik.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dvik.Pages.Courses
{
    public class DetailModel : PageModel
    {
        private readonly IData<Course> courseData;
        public Course Course { get; set; }
        public Trainer Trainer { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailModel(IData<Course> courseData)
        {
            this.courseData = courseData;
        }

        public async Task<IActionResult> OnGet(int courseId)
        {
            this.Course = await this.courseData.SearchByIdAsync(courseId);
            
            return null == this.Course 
                ? this.RedirectToPage("./NotFound") 
                : (IActionResult)this.Page();
        }
    }
}