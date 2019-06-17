using DVIK.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DVIK.Data
{
    public interface ICourseData
    {
        IEnumerable<Course> GetByName(string name);
        Course GetById(int courseId);
    }
}
