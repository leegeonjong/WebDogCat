
using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public interface IUserRepositories
    {
        //로그인 시 회원정보 가져오기
        Task<User?> GetInfo(long  id);
        //회원가입
        Task<User> AddUser(User user);
        //회원 탈퇴
        Task<User?> DeleteUser(long id);
        //회원정보 수정
        Task<User> UpdateUser(long id);

    }
}
