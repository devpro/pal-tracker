using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PalTracker.Entities;
using PalTracker.Repositories;
using Xunit;

namespace PalTrackerTests.Repositories
{
    [Collection("Integration")]
    public class MySqlTimeEntryRepositoryTest
    {
        private readonly MySqlTimeEntryRepository _repository;

        public MySqlTimeEntryRepositoryTest()
        {
            DbTestSupport.ExecuteSql("TRUNCATE TABLE time_entries");

            var builder = new DbContextOptionsBuilder<TimeEntryContext>()
                .UseMySql(DbTestSupport.TestDbConnectionString);

            _repository = new MySqlTimeEntryRepository(new TimeEntryContext(builder.Options));
        }

        [Fact]
        public void CreateInsertsATimeEntryRecord()
        {
            // Arrange
            var newTimeEntry = new TimeEntry(123, 456, DateTime.Parse("2012-01-02"), 12);

            // Act
            var createdTimeEntryId = _repository.Create(newTimeEntry).Id.Value;

            // Assert
            var foundInDb = FindInDb(createdTimeEntryId);
            foundInDb.Count.Should().Be(1);
        }

        [Fact]
        public void CreateReturnsTheCreatedTimeEntry()
        {
            // Arrange
            var newTimeEntry = new TimeEntry(123, 456, DateTime.Parse("2012-01-02"), 12);

            // Act
            var createdTimeEntry = _repository.Create(newTimeEntry);

            // Assert
            createdTimeEntry.Id.Should().NotBeNull();
            createdTimeEntry.Should().BeEquivalentTo(new TimeEntry(createdTimeEntry.Id.Value, 123, 456, DateTime.Parse("2012-01-02"), 12));
        }

        [Fact]
        public void FindFindsATimeEntry()
        {
            // Arrange
            CreateInDb(new TimeEntry(1, 123, 456, DateTime.Parse("2012-01-02"), 12));

            // Act
            var timeEntry = _repository.Find(1);

            // Assert
            timeEntry.Should().BeEquivalentTo(new TimeEntry(1L, 123L, 456L, DateTime.Parse("2012-01-02"), 12));
        }

        [Fact]
        public void ListFindsAllTimeEntries()
        {
            // Arrange
            var expected = new List<TimeEntry>
            {
                new TimeEntry(1, 111, 222, DateTime.Parse("2017-12-09"), 2),
                new TimeEntry(2, 333, 444, DateTime.Parse("2012-01-02"), 12),
                new TimeEntry(3, 555, 666, DateTime.Parse("1998-11-24"), 1)
            };
            expected.ForEach(CreateInDb);

            // Act
            var timeEntries = _repository.List();
            timeEntries.Count.Should().Be(3);

            expected.ForEach(e => timeEntries.Single(x => x.Id == e.Id).Should().BeEquivalentTo(e));
        }

        [Fact]
        public void ContainsTrueWhenPresent()
        {
            // Arrange
            CreateInDb(new TimeEntry(1, 123, 456, DateTime.Parse("2012-01-02"), 12));

            // Act & Assert
            _repository.Contains(1).Should().BeTrue();
        }

        [Fact]
        public void ContainsFalseWhenAbsent()
        {
            // Arrange
            CreateInDb(new TimeEntry(1, 123, 456, DateTime.Parse("2012-01-02"), 12));

            // Act & Assert
            _repository.Contains(2).Should().BeFalse();
        }

        [Fact]
        public void UpdateReturnsTheUpdatedRecord()
        {
            CreateInDb(new TimeEntry(1, 123, 456, DateTime.Parse("2012-01-02"), 12));

            var update = new TimeEntry(888, 999, DateTime.Parse("2014-07-29"), 1);

            var updatedTimeEntry = _repository.Update(1, update);

            Assert.Equal(1L, updatedTimeEntry.Id);
            Assert.Equal(update.ProjectId, updatedTimeEntry.ProjectId);
            Assert.Equal(update.UserId, updatedTimeEntry.UserId);
            Assert.Equal(update.Date, updatedTimeEntry.Date);
            Assert.Equal(update.Hours, updatedTimeEntry.Hours);
        }

        [Fact]
        public void UpdateUpdatesTheRecord()
        {
            CreateInDb(new TimeEntry(1, 123, 456, DateTime.Parse("2012-01-02"), 12));

            var update = new TimeEntry(888, 999, DateTime.Parse("2014-07-29"), 1);

            _repository.Update(1, update);

            var foundInDb = FindInDb(1);

            Assert.Equal(1L, foundInDb[0]["id"]);
            Assert.Equal(update.ProjectId, foundInDb[0]["project_id"]);
            Assert.Equal(update.UserId, foundInDb[0]["user_id"]);
            Assert.Equal(update.Date, foundInDb[0]["date"]);
            Assert.Equal(update.Hours, foundInDb[0]["hours"]);
            Assert.Equal(1, foundInDb.Count);
        }

        [Fact]
        public void DeleteRemovesTheRecord()
        {
            CreateInDb(new TimeEntry(1, 222, 333, DateTime.Parse("2020-12-14"), 24));
            CreateInDb(new TimeEntry(2, 444, 555, DateTime.Parse("2012-01-02"), 13));

            _repository.Delete(1);

            var foundInDb = FindAllInDb();

            Assert.Equal(1, foundInDb.Count);
            Assert.Equal(2L, foundInDb[0]["id"]);
            Assert.Equal(444L, foundInDb[0]["project_id"]);
            Assert.Equal(555L, foundInDb[0]["user_id"]);
            Assert.Equal(DateTime.Parse("2012-01-02"), foundInDb[0]["date"]);
            Assert.Equal(13, foundInDb[0]["hours"]);
        }

        private static void CreateInDb(TimeEntry timeEntry)
        {
            var sql = $@"INSERT INTO time_entries(id, project_id, user_id, date, hours)
                         VALUES('{timeEntry.Id}',
                                '{timeEntry.ProjectId}',
                                '{timeEntry.UserId}',
                                '{timeEntry.Date:yyyy-MM-dd}',
                                '{timeEntry.Hours}')";

            DbTestSupport.ExecuteSql(sql);
        }

        private static IList<IDictionary<string, object>> FindInDb(long id) => DbTestSupport.ExecuteSql(
            $@"SELECT id, project_id, user_id, date, hours
               FROM time_entries
               WHERE id = {id}"
        );

        private static IList<IDictionary<string, object>> FindAllInDb() => DbTestSupport.ExecuteSql(
            "SELECT id, project_id, user_id, date, hours FROM time_entries"
        );
    }
}
