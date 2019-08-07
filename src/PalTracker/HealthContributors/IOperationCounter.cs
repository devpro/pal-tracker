using System.Collections.Generic;

namespace PalTracker.HealthContributors
{
    public interface IOperationCounter<T>
    {
        void Increment(TrackedOperation operation);

        IDictionary<TrackedOperation, int> GetCounts { get; }

        string Name { get; }
    }
}
