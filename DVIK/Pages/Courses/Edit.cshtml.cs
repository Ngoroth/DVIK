using Dvik.Core;
using Dvik.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dvik.Pages.Courses
{
    public class EditModel : PageModel
    {
        public EditModel(DvikDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private readonly DvikDbContext dbContext;

        [BindProperty]
        public Course Course { get; set; }
        public IEnumerable<SelectListItem> TrainersNames { get; set; }
        [BindProperty]
        public string SelectedTrainerIdString { get; set; }

        public async Task<IActionResult> OnGetAsync(int? courseId)
        {
            this.Course = courseId.HasValue ? await this.dbContext.Courses.FindAsync(courseId.Value) : new Course();

            this.TrainersNames = (await this.dbContext.Trainers.ToArrayAsync())
                .Select(trainer => new SelectListItem
                {
                    Value = trainer.Id.ToString(),
                    Text = trainer.Name,
                    Selected = this.Course.Trainer?.Id == trainer.Id
                }).ToArray();

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

            this.Course.Trainer = await this.dbContext.Trainers.FindAsync(int.Parse(this.SelectedTrainerIdString));

            if(this.Course.Id > 0)
            {
                this.dbContext.Attach(this.Course).State = EntityState.Modified;
                this.Course = this.dbContext.Update(this.Course).Entity;
            }
            else
            {
                this.Course = (await this.dbContext.AddAsync(this.Course)).Entity;
            }

            await this.dbContext.SaveChangesAsync();
            this.TempData["Message"] = "Курс сохранен.";
            return this.RedirectToPage("./Details", new { courseId = this.Course.Id });
        }
    }
}