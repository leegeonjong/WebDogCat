using dogcat.Data;
using dogcat.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dogcat.Repositories
{
    public class AdminRepositories : IAdminRepositories
    {
        private readonly DogcatDbContext _dogcatDbContext;
        public AdminRepositories(DogcatDbContext dogcatDbContext)
        {
            _dogcatDbContext = dogcatDbContext;
        }
        public async Task<IEnumerable<User>> AlluserAsync(long id)  // 모든 유저 불러오기
        {
            var admin = await _dogcatDbContext.Users.FindAsync(id);
            if (admin != null)
            {
                if (admin.Admin == 1)
                {
                    var user = await _dogcatDbContext.Users.ToListAsync();
                    return user;
                }
                return null;
            }
            return null;
        }
        //public async Task<IEnumerable<User>> FindUserAsync(string id) 
        //{ 
        
        //}


        public async Task<User?> GetuserAsync(long id)
        {
            var user = await _dogcatDbContext.Users.FindAsync(id);
            if (user == null) return null;
            return user;
        }

        public async Task<User> UserBanAsync(long id)
        {
            var user = await _dogcatDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return null;
            else
            {
                if (user.Ban == 1)  // 밴 해제
                {
                    user.Ban = 0;
                    await _dogcatDbContext.SaveChangesAsync();
                    return user;
                }
                user.Ban = 1;  //밴
                await _dogcatDbContext.SaveChangesAsync();
                return user;
            }
        }
    }
}
