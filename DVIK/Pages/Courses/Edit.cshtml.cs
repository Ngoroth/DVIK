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

        [BindProperty]
        public Course Course { get; set; }
        public IActionResult OnGet(int? courseId)
        {
            if(courseId.HasValue)
            {
                this.Course = this.courseData.GetById(courseId.Value);
            }
            else
            {
                this.Course = new Course();
            }
            
            if(null == this.Course)
            {
                return this.RedirectToPage("./List");
            }
            return this.Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(this.Course.Id > 0)
            {
                this.Course = this.courseData.Update(this.Course);
            }
            else
            {
                this.Course = this.courseData.Add(this.Course);
            }

            this.courseData.Commit();
            TempData["Message"] = "Курс сохранен.";
            return this.RedirectToPage("./Detail", new { courseId = this.Course.Id });
        }
    }
}