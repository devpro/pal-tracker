using System.Collections.Generic;
using System.Linq;
using PalTracker.Entities;

namespace PalTracker.Repositories
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
        private readonly IDictionary<long, TimeEntry> _timeEntries = new Dictionary<long, TimeEntry>();

        public bool Contains(long id)
        {
            return _timeEntries.Keys.Contains(id);
        }

        public TimeEntry Find(long id)
        {
            return _timeEntries[id];
        }

        public List<TimeEntry> List()
        {
            return _timeEntries.Values.ToList();
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var id = _timeEntries.Count + 1;
            timeEntry.Id = id;
            _timeEntries.Add(id, timeEntry);
            return timeEntry;
        }

        public TimeEntry Update(long id, TimeEntry newTimeEntry)
        {
            newTimeEntry.Id = id;
            _timeEntries[id] = newTimeEntry;
            return newTimeEntry;
        }

        public void Delete(long id)
        {
            _timeEntries.Remove(id);
        }
    }
}
