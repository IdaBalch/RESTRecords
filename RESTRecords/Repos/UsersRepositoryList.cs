using Microsoft.AspNetCore.Identity;
using RESTRecords.Models;
using System.Numerics;

namespace RESTRecords.Repos
{
    public class UsersRepositoryList
    {

        private readonly List<User> _users = new();

        private int _nextId = 1;

        public UsersRepositoryList(bool includesTestData = false)
        {
            if (includesTestData)
            {
                var hasher = new PasswordHasher<User>();

                var admin = new User { Username = "admin", Role = "Admin" };
                admin.PasswordHash = hasher.HashPassword(admin, "1234");
                AddUser(admin);

                var user = new User { Username = "user", Role = "User" };
                user.PasswordHash = hasher.HashPassword(user, "1234");
                AddUser(user);
            }
        }

        public User? GetByUsername(string username)
        {
            return _users.Find(user =>
                user.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase));
        }

        public User AddUser(User user)
        {
            if (GetByUsername(user.Username) != null)
            {
                throw new ArgumentException(
                    "Brugernavnet findes allerede.");
            }
            user.Id = _nextId++;
            _users.Add(user);

            return user;
        }

    }
}
