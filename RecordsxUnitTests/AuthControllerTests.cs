using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RESTRecords.Controllers;
using RESTRecords.Models;
using RESTRecords.Repos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RecordsxUnitTests
{
    public class AuthControllerTests
    {
        // Independent test settings; these are not the application's JWT secrets.
        private readonly IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = new string('x', 64),
                ["Jwt:Issuer"] = "UnitTests",
                ["Jwt:Audience"] = "UnitTestsClient"
            })
            .Build();

        [Theory]
        [InlineData("admin", "Admin")]
        [InlineData("user", "User")]
        public void Login_WithCorrectPassword_ReturnsValidTokenWithUsersRole(
            string username, string role)
        {
            // Arrange
            var repo = new UsersRepositoryList(true);
            var controller = new AuthController(config, repo);

            // Act
            var result = controller.Login(new LoginRequest
            {
                Username = username,
                Password = "1234"
            });

            // Assert
            AssertToken(result, username, role);
        }

        [Theory]
        [InlineData("admin", "wrong-password")]
        [InlineData("user", "wrong-password")]
        [InlineData("unknown-user", "1234")]
        public void Login_WithInvalidCredentials_ReturnsUnauthorized(
            string username, string password)
        {
            var controller = new AuthController(config, new UsersRepositoryList(true));

            var result = controller.Login(new LoginRequest
            {
                Username = username,
                Password = password
            });

            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public void Login_UsesUserAddedToRepository()
        {
            // This user is not one of the constructor's example users.
            var repo = new UsersRepositoryList();
            var user = new User { Username = "student", Role = "User" };
            user.PasswordHash = new PasswordHasher<User>()
                .HashPassword(user, "student-password");
            repo.AddUser(user);
            var controller = new AuthController(config, repo);

            var result = controller.Login(new LoginRequest
            {
                Username = "student",
                Password = "student-password"
            });

            AssertToken(result, "student", "User");
        }

        private void AssertToken(IActionResult result, string username, string role)
        {
            var ok = Assert.IsType<OkObjectResult>(result);
            var response = JsonSerializer.SerializeToElement(ok.Value);
            string? token = response.GetProperty("token").GetString();
            Assert.False(string.IsNullOrWhiteSpace(token));

            var principal = new JwtSecurityTokenHandler().ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                }, out _);

            Assert.Equal(username, principal.Identity?.Name);
            Assert.Equal(role, principal.FindFirst(ClaimTypes.Role)?.Value);
        }
    }
}
