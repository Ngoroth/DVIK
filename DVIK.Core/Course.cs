using DA = System.ComponentModel.DataAnnotations;

namespace Dvik.Core
{
    public class Course
    {
        public int Id { get; set; }
        [DA.Required(ErrorMessage = "Необходимо ввести название курса!")]
        public string Name { get; set; }
        [DA.Required(ErrorMessage = "Необходимо ввести описание курса!")]
        public string Description { get; set; }
        public Trainer Trainer { get; set; }

        public decimal Price { get; set; }
    }
}
