using System.Collections.Generic;

namespace Dvik.Core.Abstractions
{
    public interface ICourseData
    {
        IEnumerable<Course> GetByName(string name);
        Course GetById(int courseId);
        Course Update(Course updatedCourse);
        Course Add(Course newCourse);
        Course Delete(int id);
        int Commit();
    }
}
