using System.Collections.Generic;
using System.Linq;
using Dvik.Core;
using Dvik.Core.Abstractions;

namespace Dvik.Data
{
    public class InMemoryCourseData : ICourseData
    {
        public List<Course> Courses { get; set; }

        public InMemoryCourseData()
        {
            this.Courses = new List<Course>
            {
                new Course { Name = "Продаем все", Description = "Научим продавать все!", Id = 0, Price = 2000},
                new Course { Name = "Заставь себя уважать", Description = "Заставим всех уважать тебя!", Id = 1, Price = 2000 },
                new Course { Name = "Болтай без умолку", Description = "Будешь трындеть как радио и делать это круто!", Id = 2, Price = 2000}
            };
        }
        public IEnumerable<Course> SearchByName(string name = null)
        {
            return this.Courses
                .Where(c => string.IsNullOrEmpty(name) || c.Name.StartsWith(name))
                .OrderBy(c => c.Name);
        }

        public Course SearchById(int courseId)
        {
            return this.Courses.SingleOrDefault(c => c.Id == courseId);
        }

        public Course Update(Course updatedCourse)
        {
            var course = this.Courses.SingleOrDefault(c => c.Id == updatedCourse.Id);
            if(null == course)
            {
                return null;
            }
            course.Name = updatedCourse.Name;
            course.Price = updatedCourse.Price;
            course.Trainer = updatedCourse.Trainer;
            course.Description = updatedCourse.Description;
            return course;
        }

        public int Commit()
        {
            return 0;
        }

        public Course Add(Course newCourse)
        {
            this.Courses.Add(newCourse);
            newCourse.Id = this.Courses.Max(c => c.Id) + 1;
            return newCourse;
        }

        public Course Delete(int id)
        {
            var course = this.Courses.FirstOrDefault(c => c.Id == id);
            if(course != null)
            {
                this.Courses.Remove(course);
            }
            return course;
        }
    }
}
