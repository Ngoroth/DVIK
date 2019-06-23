using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using Dvik.Core.Abstractions;
using System.Linq;

namespace Dvik.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly IData<Trainer> trainerData;

        public IndexModel(IData<Trainer> trainerData)
        {
            this.trainerData = trainerData;
        }

        public List<Trainer> Trainers { get;set; }


        public async Task OnGetAsync()
        {
            this.Trainers = (await this.trainerData.SearchByNameAsync(string.Empty)).ToList();
        }
    }
}
