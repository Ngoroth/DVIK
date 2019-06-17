using System.Collections.Generic;

namespace Dvik.Core.Abstractions
{
    public interface ICourseData
    {
        IEnumerable<Course> GetByName(string name);
        Course GetById(int courseId);
    }
}
