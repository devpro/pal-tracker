using System.Collections.Generic;
using PalTracker.Entities;

namespace PalTracker.Repositories
{
    public interface ITimeEntryRepository
    {
        bool Contains(long id);
        TimeEntry Find(long id);
        List<TimeEntry> List();
        TimeEntry Create(TimeEntry toCreate);
        TimeEntry Update(long id, TimeEntry theUpdate);
        void Delete(long id);
    }
}
