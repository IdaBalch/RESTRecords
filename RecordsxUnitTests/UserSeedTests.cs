using Microsoft.AspNetCore.Identity;
using RESTRecords.Models;
using RESTRecords.Repos;

namespace RecordsxUnitTests
{
    public class UserSeedTests
    {
        [Theory]
        [InlineData("admin", "Admin", 1)]
        [InlineData("user", "User", 2)]
        public void Constructor_WithTestData_CreatesUserWithHashedPassword(
            string username, string role, int id)
        {
            // Arrange
            var repo = new UsersRepositoryList(true);

            // Act
            User? user = repo.GetByUsername(username);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(id, user.Id);
            Assert.Equal(role, user.Role);
            Assert.NotEqual("1234", user.PasswordHash);

            var hasher = new PasswordHasher<User>();
            Assert.NotEqual(PasswordVerificationResult.Failed,
                hasher.VerifyHashedPassword(user, user.PasswordHash, "1234"));
        }

        [Fact]
        public void Constructor_WithoutTestData_DoesNotCreateExampleUsers()
        {
            var repo = new UsersRepositoryList();

            Assert.Null(repo.GetByUsername("admin"));
            Assert.Null(repo.GetByUsername("user"));
        }
    }
}
