using RESTRecords.Models;
using System.Numerics;

namespace RESTRecords.Repos
{
    public class UsersRepositoryList
    {

        private readonly List<User> _users = new();

        private int _nextId = 1;
        public User? GetByUsername(string username)
        {
            return _users.Find(user =>
                user.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase));
        }

        public User Add(User user)
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
