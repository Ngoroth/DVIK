using Dvik.Core;
using Dvik.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dvik.Pages.Courses
{
    public class EditModel : PageModel
    {
        public EditModel(ICourseData courseData)
        {
            this.courseData = courseData;
        }

        private readonly ICourseData courseData;

        public Course Course { get; set; }
        public IActionResult OnGet(int courseId)
        {
            this.Course = this.courseData.GetById(courseId);
            if(null == this.Course)
            {
                return this.RedirectToPage("./List");
            }
            return this.Page();
        }
    }
}