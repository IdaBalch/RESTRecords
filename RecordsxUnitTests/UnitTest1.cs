using System;
using RESTRecords.Models;
using RESTRecords.Repos;
namespace RecordsxUnitTests
{
    public class UnitTest1
    {

        private readonly RecordsRepositoryList _repo;

        public UnitTest1()
        {
            _repo = new RecordsRepositoryList();
        }
        [Fact]

        public void Constructor_WithTestData_ShouldInitializeWithTestData()
        {
            // Arrange
            var repo = new RecordsRepositoryList(true);
            // Act
            var records = repo.GetAllRecords();
            // Assert
            Assert.NotNull(records);
            Assert.Equal(3, records.Count());
        }

        [Fact]
        public void Constructor_WithoutTestData_ShouldInitializeEmpty()
        {
            // Arrange
            var repo = new RecordsRepositoryList(false);
            // Act
            var records = repo.GetAllRecords();
            // Assert
            Assert.NotNull(records);
            Assert.Empty(records);
        }

        [Fact]
        public void AddRecords_ShouldAddRecordAndAssignId()
        {
            // Arrange
            var repo = new RecordsRepositoryList();
            var newRecord = new Records { Title = "New Song", Artist = "New Artist", Duration = 200, PublicationYear = 2022 };
            // Act
            var addedRecord = repo.AddRecords(newRecord);
            var records = repo.GetAllRecords();
            // Assert
            Assert.NotNull(addedRecord);
            Assert.Equal(1, addedRecord.Id);
            Assert.Single(records);
            Assert.Equal("New Song", records.First().Title);
        }

        [Fact]
        public void TestGetAll_ReturnsAllRecords()
        {
            // Arrange
            var repo = new RecordsRepositoryList(true);

            // Act
            var records = repo.GetAllRecords();

            // Assert

            Assert.NotNull(records);
        }

        [Fact]
        public void TestGetAll_ReturnsEmptyListWhenNoRecords()
        {
            // Arrange
            var repo = new RecordsRepositoryList(false);
            // Act
            var records = repo.GetAllRecords();
            // Assert
            Assert.NotNull(records);
            Assert.Empty(records);
        }


        [Fact]
        public void GetAllRecords_FilterByTitle_ShouldReturnMatchingRecords()
        {
            // Arrange
            var repo = new RecordsRepositoryList(true);

            // Act
            var result = repo.GetAllRecords(title: "Song 1").ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Song 1", result[0].Title);
        }

        [Fact]
        public void GetAllRecords_FilterByArtist_ShouldReturnMatchingRecords()
        {
            // Arrange
            var repo = new RecordsRepositoryList(true);

            // Act
            var result = repo.GetAllRecords(artist: "Artist A").ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Artist A", result[0].Artist);
        }

        [Fact]
        public void GetAllRecords_FilterByMaxDuration_ShouldReturnRecordsAtOrBelowLimit()
        {
            // Arrange
            var repo = new RecordsRepositoryList(true);

            // Act
            var result = repo.GetAllRecords(maxDuration: 210).ToList();

            // Assert
            Assert.Equal(new[] { 180, 210 }, result.Select(record => record.Duration).OrderBy(duration => duration));
        }

        [Fact]
        public void GetAllRecords_FilterByPublicationYear_ShouldReturnMatchingRecords()
        {
            // Arrange
            var repo = new RecordsRepositoryList(true);

            // Act
            var result = repo.GetAllRecords(publicationYear: 2020).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal(2020, result[0].PublicationYear);
        }

        [Fact]
        public void GetAllRecords_FilterByMultipleParameters_ShouldReturnMatchingRecords()
        {
            // Arrange
            var repo = new RecordsRepositoryList(true);

            // Act
            var result = repo.GetAllRecords(
                artist: "Artist A",
                publicationYear: 2020
            ).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Song 1", result[0].Title);
        }

        [Fact]
        public void GetAllRecords_FilterByNonExistingTitle_ShouldReturnEmptyList()
        {
            // Arrange
            var repo = new RecordsRepositoryList(true);

            // Act
            var result = repo.GetAllRecords(title: "NonExistingSong").ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
