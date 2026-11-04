using RESTRecords.Repos;
using RESTRecords.Models; 
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordsxUnitTests
{
    public class UserRepositoryTest
    {
        private readonly UsersRepositoryList _userRepo;

        public UserRepositoryTest()
        {
            _userRepo = new UsersRepositoryList();
        }

        [Fact]
        public void Test_GetByUsername_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var testUser = new User()
            {
                Username = "testuser",
                PasswordHash = "testhash",
                Role = "Admin"
            };


            //Act 

            User addedUser = _userRepo.AddUser(testUser);
            User? foundUser = _userRepo.GetByUsername("testuser");

            //Assert
            Assert.Equal(1, addedUser.Id);
            Assert.NotNull(foundUser);
            Assert.Equal("testuser", foundUser.Username);
            Assert.Equal("testhash", foundUser.PasswordHash);
            Assert.Equal("Admin", foundUser.Role);

        }


        [Fact]
        public void Test_AddUser_ShouldThrowException_WhenUsernameAlreadyExists()
        {
            // Arrange
            var testUser1 = new User()
            {
                Username = "testuser",
                PasswordHash = "testhash1",
                Role = "Admin"
            };
            var testUser2 = new User()
            {
                Username = "testuser", // Same username as testUser1
                PasswordHash = "testhash2",
                Role = "User"
            };
            // Act
            _userRepo.AddUser(testUser1);
            // Assert
            var exception = Assert.Throws<ArgumentException>(() => _userRepo.AddUser(testUser2));
            Assert.Equal("Brugernavnet findes allerede.", exception.Message);
        }


        [Fact]
        public void Test_GetByUsername_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            User? foundUser = _userRepo.GetByUsername("nonexistentuser");
            // Assert
            Assert.Null(foundUser);
        }

        [Fact] 
        public void Test_AddUser_ShouldAssignUniqueId()
        {
            // Arrange
            var testUser1 = new User()
            {
                Username = "user1",
                PasswordHash = "hash1",
                Role = "Admin"
            };
            var testUser2 = new User()
            {
                Username = "user2",
                PasswordHash = "hash2",
                Role = "User"
            };
            // Act
            User addedUser1 = _userRepo.AddUser(testUser1);
            User addedUser2 = _userRepo.AddUser(testUser2);
            // Assert
            Assert.Equal(1, addedUser1.Id);
            Assert.Equal(2, addedUser2.Id);
        }




    }
}
