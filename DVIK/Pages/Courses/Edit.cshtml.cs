using Dvik.Core;
using Dvik.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dvik.Pages.Courses
{
    public class EditModel : PageModel
    {
        public EditModel(IData<Course> courseData, IData<Trainer> trainerData)
        {
            this.courseData = courseData;
            this.trainerData = trainerData;
        }

        private readonly IData<Course> courseData;
        private readonly IData<Trainer> trainerData;

        [BindProperty]
        public Course Course { get; set; }
        public IEnumerable<SelectListItem> TrainersNames { get; set; }
        [BindProperty]
        public string SelectedTrainerIdString { get; set; }

        public async Task<IActionResult> OnGetAsync(int? courseId)
        {
            this.Course = courseId.HasValue ? await this.courseData.SearchByIdAsync(courseId.Value) : new Course();
            this.TrainersNames = (await trainerData.SearchByNameAsync(""))
                .Select(trainer => new SelectListItem
                {
                    Value = trainer.Id.ToString(),
                    Text = trainer.Name,
                    Selected = this.Course.Trainer?.Id == trainer.Id
                });
            return null == this.Course 
                ? this.RedirectToPage("./List") 
                : (IActionResult)this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!this.ModelState.IsValid)
            {
                return this.Page();
            }
            this.Course.Trainer = await this.trainerData.SearchByIdAsync(int.Parse(this.SelectedTrainerIdString));
            this.Course = this.Course.Id > 0 
                ? this.courseData.Update(this.Course) 
                : await this.courseData.AddAsync(this.Course);

            await this.courseData.CommitAsync();
            this.TempData["Message"] = "Курс сохранен.";
            return this.RedirectToPage("./Details", new { courseId = this.Course.Id });
        }
    }
}