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
    }
}
