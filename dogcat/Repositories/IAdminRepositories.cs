using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public interface IAdminRepositories
    {
        Task<IEnumerable<User>> AlluserAsync(long id);  // 모든 유저를 불러온다

        Task<User> UserBanAsync(long id);  // 특정 유저를 벤한다.

        Task<User?> GetuserAsync(long id);  // 특정 유저의 정보를 가져온다. 

         // 유저가 관리자 권한이 있는지


    }
}
