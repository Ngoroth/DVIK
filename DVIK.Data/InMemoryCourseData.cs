using System.Collections.Generic;
using System.Linq;
using Dvik.Core;
using Dvik.Core.Abstractions;

namespace Dvik.Data
{
    public class InMemoryCourseData : ICourseData
    {
        public IEnumerable<Course> Courses { get; set; }

        public InMemoryCourseData()
        {
            this.Courses = new[]
            {
                new Course { Name = "Продаем все", Description = "Научим продавать все!", Id = 0, Price = 2000},
                new Course { Name = "Заставь себя уважать", Description = "Заставим всех уважать тебя!", Id = 1, Price = 2000 },
                new Course { Name = "Болтай без умолку", Description = "Будешь трындеть как радио и делать это круто!", Id = 2, Price = 2000}
            };
        }
        public IEnumerable<Course> GetByName(string name = null)
        {
            return this.Courses
                .Where(c => string.IsNullOrEmpty(name) || c.Name.StartsWith(name))
                .OrderBy(c => c.Name);
        }

        public Course GetById(int courseId)
        {
            return this.Courses.SingleOrDefault(c => c.Id == courseId);
        }
    }
}
