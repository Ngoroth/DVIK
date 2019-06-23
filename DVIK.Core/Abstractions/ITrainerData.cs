using System.Collections.Generic;

namespace Dvik.Core.Abstractions
{
    public interface ITrainerData
    {
        IEnumerable<Trainer> SearchByName(string name);
        Trainer SearchById(int courseId);
        Trainer Update(Trainer updatedCourse);
        Trainer Add(Trainer newCourse);
        Trainer Delete(int id);
        int Commit();
    }
}
