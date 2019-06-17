using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DVIK.Core;
using DVIK.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DVIK.Pages.Courses
{
    public class ListModel : PageModel
    {
        private readonly ICourseData courseData;

        public IEnumerable<Course> Courses { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public ListModel(ICourseData courseData)
        {
            this.courseData = courseData;
        }
        public void OnGet()
        {
            this.Courses = this.courseData.GetByName(this.SearchTerm);
        }
    }
}