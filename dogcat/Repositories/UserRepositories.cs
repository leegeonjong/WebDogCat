using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public class UserRepositories : IUserRepositories
    {
        public Task<User> AddUser(User user)
        {
            throw new NotImplementedException();
        }
        public Task<User?> GetInfo(long id)
        {
            throw new NotImplementedException();
        }
        public Task<User> UpdateUser(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> DeleteUser(long id)
        {
            throw new NotImplementedException();
        }


    }
}
