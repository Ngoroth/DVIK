using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using Microsoft.AspNetCore.Http;
using System.IO;
using Dvik.Data;
using Microsoft.EntityFrameworkCore;

namespace Dvik.Pages.Trainers
{
    public class EditModel : PageModel
    {
        private readonly DvikDbContext dbContext;

        public EditModel(DvikDbContext trainerData)
        {
            this.dbContext = trainerData;
        }

        [BindProperty]
        public Trainer Trainer { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? trainerId)
        {
            this.Trainer = trainerId.HasValue 
                ? await this.dbContext.Trainers.FindAsync(trainerId.Value) 
                : new Trainer();

            return null == this.Trainer
                ? this.RedirectToPage("./List")
                : (IActionResult)this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.addPhoto();

            if (this.Trainer.Id > 0)
            {
                this.dbContext.Attach(this.Trainer).State = EntityState.Modified;
                this.Trainer = this.dbContext.Update(this.Trainer).Entity;
            }
            else
            {
                this.Trainer = (await this.dbContext.AddAsync(this.Trainer)).Entity;
            }

            await this.dbContext.SaveChangesAsync();
            this.TempData["Message"] = "Тренер сохранен.";
            return this.RedirectToPage("./Details", new { trainerId = this.Trainer.Id });
        }

        private void addPhoto()
        {
            if (this.Photo == null)
            {
                return;
            }

            this.Trainer.Photo = new Photo();
            using (var binaryReader = new BinaryReader(this.Photo.OpenReadStream()))
            {
                this.Trainer.Photo.Data = binaryReader.ReadBytes((int)this.Photo.Length);
            }
        }
    }
}
