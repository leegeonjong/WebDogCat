using dogcat.Data;
using dogcat.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dogcat.Repositories
{
    public class MessageRepositories : IMessageRepositories
    {
        private readonly DogcatDbContext _dogcatDbContext;
        public MessageRepositories(DogcatDbContext dogcatDbContext)
        {
            _dogcatDbContext = dogcatDbContext;
        }

        public async Task<User> MailAsync(string mail)
        {
            var user = await _dogcatDbContext.Users.FirstOrDefaultAsync(x => x.Mail == mail);
            if (user == null) return null;
            return user;
        }
        public async Task<User> UserAsync(long id)
        {
            var user = await _dogcatDbContext.Users.FindAsync(id);

            return user;
        }
        public async Task<Message> McontextAsync(long id)
        {
            var message = await _dogcatDbContext.Messages.FindAsync(id);
            var user_from = await _dogcatDbContext.Users.FirstOrDefaultAsync(x => x.Id == message.From_id);
            message.User_from = user_from;
            return message;
        }

        public async Task<IEnumerable<Message>> MessageAsync(long id)
        {
            var message = await _dogcatDbContext.Messages.Where(x => x.To_id == id).ToListAsync();
            if (message.Count == 0) return null;
            foreach(var m  in message)
            {
                var user_from = await _dogcatDbContext.Users.FirstOrDefaultAsync(x => x.Id == m.From_id);
                var user_to = await _dogcatDbContext.Users.FirstOrDefaultAsync(x => x.Id == m.To_id);
                m.User_from = user_from;
                m.User_to = user_to;
            }
            return message;
        }
        public async Task<Message> AddAsync(Message m)
        {
            await _dogcatDbContext.Messages.AddAsync(m);
            await _dogcatDbContext.SaveChangesAsync();
            return m;
        }
        
    }
}
