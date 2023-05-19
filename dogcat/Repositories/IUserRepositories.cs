
using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public interface IUserRepositories 
    {
        Task<User> GetUserAsync(long id);  // 유저를 찾아온다.
        Task<User?> UpdateAsync(User user);  // 유저 정보 수정
        //로그인 시 회원정보 가져오기
        Task<User?> GetInfoAsync(long  id);
        //회원가입
        Task<User> AddUserAsync(User user);
        //회원 탈퇴
        Task<User?> DeleteUserAsync(long id);
        //회원정보 수정
        Task<User> UpdateUserAsync(long id);

        Task<User?> DeleteAsync(long id);  // 유저 탈퇴
    }
}
