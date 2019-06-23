using System.Collections.Generic;
using System.Threading.Tasks;
using Dvik.Core;
using Dvik.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dvik.Pages.Courses
{
    public class ListModel : PageModel
    {
        private readonly IData<Course> courseData;

        public IEnumerable<Course> Courses { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public ListModel(IData<Course> courseData)
        {
            this.courseData = courseData;
        }
        public async Task OnGet()
        {
            this.Courses = await this.courseData.SearchByNameAsync(this.SearchTerm);
        }
    }
}