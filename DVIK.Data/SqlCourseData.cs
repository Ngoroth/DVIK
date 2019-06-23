﻿using Dvik.Core;
using Dvik.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dvik.Data
{
    public class SqlCourseData : IData<Course>
    {
        private readonly DvikDbContext dvikDbContext;

        public SqlCourseData(DvikDbContext dvikDbContext)
        {
            this.dvikDbContext = dvikDbContext;
        }

        public async Task<Course> AddAsync(Course newCourse)
        {
            await this.dvikDbContext.AddAsync(newCourse);
            return newCourse;
        }

        public async Task<int> CommitAsync()
        {
            return await this.dvikDbContext.SaveChangesAsync();
        }

        public async Task<Course> DeleteAsync(int id)
        {
            var course = await this.SearchByIdAsync(id);

            if (course != null)
            {
                this.dvikDbContext.Courses.Remove(course);
            }
            return course;
        }

        public async Task<Course> SearchByIdAsync(int courseId)
        {
            return await this.dvikDbContext.Courses.FindAsync(courseId);
        }

        public async Task<IEnumerable<Course>> SearchByNameAsync(string name)
        {
            var query = this.dvikDbContext.Courses
                .Where(c => c.Name.StartsWith(name) || string.IsNullOrEmpty(name))
                .OrderBy(c => c.Name);
            return await query.ToArrayAsync();
        }

        public Course Update(Course updatedCourse)
        {
            var entity = this.dvikDbContext.Attach(updatedCourse);
            entity.State = EntityState.Modified;
            return updatedCourse;
        }
    }
}
