using dogcat.Data;
using dogcat.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace dogcat.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DogcatDbContext commentDbContext;

        public CommentRepository(DogcatDbContext commentDbContext)
        {
            Console.WriteLine("WriteRepoitory() 생성");
            this.commentDbContext = commentDbContext;
        }
        public async Task<Comment?> CommentDeleteAsync(long id)
        {
            var existingComment = await commentDbContext.Comments.FindAsync(id);
            if (existingComment != null)
            {
                commentDbContext.Comments.Remove(existingComment);
                await commentDbContext.SaveChangesAsync();  // DELETE
                return existingComment;
            }
            return null;
        }

        public Task<IEnumerable<Comment>> CommentGetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> CommentGetAsync(long Writeid)
        {
            return await commentDbContext.Comments
                .Where(c => c.WriteId == Writeid)
                .ToListAsync();
        }

        public Task<Comment?> CommentUpdateAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<Comment> CommnetAddAsync(Comment comment)
        {
            await commentDbContext.Comments.AddAsync(comment);
            await commentDbContext.SaveChangesAsync();
            return comment;
        }
    }
}
