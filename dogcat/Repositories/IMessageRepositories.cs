using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public interface IMessageRepositories
    {
        Task<IEnumerable<Message>> MessageAsync(long id);  // 메시지함 

        Task<Message> McontextAsync(long id);  // 쪽지 가져오기

        Task<User> UserAsync(long id);

        Task<User> MailAsync(string mail);  // mail 로 유저 가져오기

        Task<Message> AddAsync(Message m);  // mail 보내기
    }
}
