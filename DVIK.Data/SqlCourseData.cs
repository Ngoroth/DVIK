using Dvik.Core;
using Dvik.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dvik.Data
{
    public class SqlCourseData : ICourseData
    {
        private readonly DvikDbContext dvikDbContext;

        public SqlCourseData(DvikDbContext dvikDbContext)
        {
            this.dvikDbContext = dvikDbContext;
        }

        public Course Add(Course newCourse)
        {
            this.dvikDbContext.Add(newCourse);
            return newCourse;
        }

        public int Commit()
        {
            return this.dvikDbContext.SaveChanges();
        }

        public Course Delete(int id)
        {
            var course = this.GetById(id);

            if(course != null)
            {
                this.dvikDbContext.Courses.Remove(course);
            }
            return course;
        }

        public Course GetById(int courseId)
        {
            return this.dvikDbContext.Courses.Find(courseId);
        }

        public IEnumerable<Course> GetByName(string name)
        {
            var query = this.dvikDbContext.Courses
                .Where(c => c.Name.StartsWith(name) || string.IsNullOrEmpty(name))
                .OrderBy(c => c.Name);
            return query.ToArray();
        }

        public Course Update(Course updatedCourse)
        {
            var entity = this.dvikDbContext.Attach(updatedCourse);
            entity.State = EntityState.Modified;
            return updatedCourse;
        }
    }
}
