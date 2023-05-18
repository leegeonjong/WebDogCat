﻿using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public interface IUserRepositories 
    {
        Task<User> GetUserAsync(long id);  // 유저를 찾아온다.
        Task<User?> UpdateAsync(User user);  // 유저 정보 수정

        Task<User?> DeleteAsync(long id);  // 유저 탈퇴
    }
}
