using dogcat.Data;
using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public class UserRepository : IUserRepositories
    {
        private readonly DogcatDbContext dc_context;
        public UserRepository(DogcatDbContext dc_context)
        {
            Console.WriteLine("유저Repo생성");
            this.dc_context = dc_context;
        }
        public Task<User> AddUser(User user) //가입
        {
            throw new NotImplementedException();
        }
        public async Task<User?> GetInfo(long id) //수정,삭제시 필요 : 유저id 가져오기
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
