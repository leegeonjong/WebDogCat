using dogcat.Data;
using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public class UserRepository : IUserRepositories
    {
        //여기서 sql동작 수행
        private readonly DogcatDbContext db_context;
        public UserRepository(DogcatDbContext db_context)
        {
            Console.WriteLine("유저Repo생성");
            this.db_context = db_context;
        }
        public async Task<User> AddUserAsync(User user) //가입
        {
           await db_context.Users.AddAsync(user);
            await db_context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> GetInfoAsync(long id) //수정,삭제시 필요 : 유저id 가져오기
        {
            throw new NotImplementedException();
        }
        public Task<User> UpdateUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> DeleteUserAsync(long id)
        {
            throw new NotImplementedException();
        }


    }
}
