using System;
using FluentAssertions;
using PalTracker.Entities;
using PalTracker.Repositories;
using Xunit;

namespace PalTrackerTests.Repositories
{
    public class InMemoryTimeEntryRepositoryTest
    {
        private readonly InMemoryTimeEntryRepository _repository;

        public InMemoryTimeEntryRepositoryTest()
        {
            _repository = new InMemoryTimeEntryRepository();
        }

        [Fact]
        public void Create()
        {
            // Act
            var created = _repository.Create(new TimeEntry(222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24));

            // Assert
            var expected = new TimeEntry(1, 222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24);
            created.Should().BeEquivalentTo(expected);
            _repository.Find(1).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Find()
        {
            // Arrange
            _repository.Create(new TimeEntry(222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24));

            // Act
            var found = _repository.Find(1);

            // Assert
            var expected = new TimeEntry(1, 222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24);
            found.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Contains()
        {
            // Arrange
            _repository.Create(new TimeEntry(222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24));

            // Act & Assert
            _repository.Contains(1).Should().BeTrue();
            _repository.Contains(2).Should().BeFalse();
        }

        [Fact]
        public void List()
        {
            // Arrange
            _repository.Create(new TimeEntry(222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24));
            _repository.Create(new TimeEntry(888, 777, new DateTime(2012, 09, 02, 11, 30, 00), 12));

            // Act
            var found = _repository.List();

            // Assert
            found.Count.Should().Be(2);
            found[0].Should().BeEquivalentTo(new TimeEntry(1, 222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24));
            found[1].Should().BeEquivalentTo(new TimeEntry(2, 888, 777, new DateTime(2012, 09, 02, 11, 30, 00), 12));
        }

        [Fact]
        public void Update()
        {
            // Arrange
            _repository.Create(new TimeEntry(222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24));

            // Act
            _repository.Update(1, new TimeEntry(555, 666, new DateTime(2020, 08, 01, 01, 55, 10), 8));

            // Assert
            var entries = _repository.List();
            entries.Count.Should().Be(1);
            entries[0].Should().BeEquivalentTo(new TimeEntry(1, 555, 666, new DateTime(2020, 08, 01, 01, 55, 10), 8));
        }

        [Fact]
        public void Delete()
        {
            // Arrange
            _repository.Create(new TimeEntry(222, 333, new DateTime(2008, 08, 01, 12, 00, 01), 24));
            _repository.Create(new TimeEntry(888, 777, new DateTime(2012, 09, 02, 11, 30, 00), 12));

            // Act
            _repository.Delete(1);

            // Assert
            var entries = _repository.List();
            entries.Count.Should().Be(1);
            entries[0].Should().BeEquivalentTo(new TimeEntry(2, 888, 777, new DateTime(2012, 09, 02, 11, 30, 00), 12));
        }
    }
}
