using dogcat.Data;
using dogcat.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dogcat.Repositories
{
    public class UserRepositories : IUserRepositories
    {
        private readonly DogcatDbContext _dogcatDbContext;
        public UserRepositories(DogcatDbContext dogcatDbContext)
        {
            _dogcatDbContext = dogcatDbContext;
        }

        public async Task<User?> DeleteAsync(long id)
        {
            var users = await _dogcatDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(users == null) return null;
            _dogcatDbContext.Users.Remove(users);
            await _dogcatDbContext.SaveChangesAsync();
            return users;
        }

        public async Task<User> GetUserAsync(long id)  // 유저 정보 가져오기
        {
            var users = await _dogcatDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(users == null) return null;
            return users;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var users = await _dogcatDbContext.Users.FindAsync(user.Id);
            if(users == null) return null;
            else
            {
                users.Userid = user.Userid;
                users.Pw = user.Pw;
                users.Name= user.Name;
                users.NickName = user.NickName;
                users.PhoneNum = user.PhoneNum;
                users.Mail = user.Mail;
            }
            await _dogcatDbContext.SaveChangesAsync();
            return users;
        }
    }
}
