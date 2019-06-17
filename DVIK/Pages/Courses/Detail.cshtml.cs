using Dvik.Core;
using Dvik.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dvik.Pages.Courses
{
    public class DetailModel : PageModel
    {
        private readonly ICourseData courseData;
        public Course Course { get; set; }

        public DetailModel(ICourseData courseData)
        {
            this.courseData = courseData;
        }

        public IActionResult OnGet(int courseId)
        {
            this.Course = this.courseData.GetById(courseId);

            if(null == this.Course)
            {
                return this.RedirectToPage("./NotFound");
            }

            return this.Page();
        }
    }
}