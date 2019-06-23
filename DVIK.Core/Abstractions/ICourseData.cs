using System.Collections.Generic;

namespace Dvik.Core.Abstractions
{
    public interface ICourseData
    {
        IEnumerable<Course> SearchByName(string name);
        Course SearchById(int courseId);
        Course Update(Course updatedCourse);
        Course Add(Course newCourse);
        Course Delete(int id);
        int Commit();
    }
}
