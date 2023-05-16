using dogcat.Data;
using dogcat.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dogcat.Repositories
{
    public class writeRepository : IWriteRepository
    {
        private readonly DogcatDbContext writeDbContext;
        
        public writeRepository(DogcatDbContext writeDbContext) //writeRepository 초기화
        {
            Console.WriteLine("WriteRepoitory() 생성");
            this.writeDbContext = writeDbContext;
        }

        public async Task<Write> AddAsync(Write write) //새로운 글 추가
        {
            await writeDbContext.Writes.AddAsync(write);
            await writeDbContext.SaveChangesAsync();
            return write;
        }

        public async Task<long> CountAsync()
        {
            return await writeDbContext.Writes.CountAsync();
        }

        public async Task<Write?> DeleteAsync(long id) //새로운 글 삭제
        {
            var existingWrite = await writeDbContext.Writes.FindAsync(id);
            if (existingWrite != null)
            {
                writeDbContext.Writes.Remove(existingWrite);
                await writeDbContext.SaveChangesAsync();  // DELETE
                return existingWrite;
            }
            return null;
        }

        public async Task<IEnumerable<Write>> GetAllAsync()
        {
            var writes = await writeDbContext.Writes.ToListAsync();
            return writes.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<Write?> GetAsync(long id)
        {
            return await writeDbContext.Writes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Write>> GetFromRowAsync(int fromRow, int pageRows)
        {
            return await writeDbContext.Writes
                .OrderByDescending(x => x.Id)  // 최신순으로
                .Skip(fromRow)    // fromRow 번째 부터
                .Take(pageRows)   // pageRows 개를 SELECT
                .ToListAsync();
        }

        public async Task<Write?> IncViewCntAsync(long id)
        {
            var existingWrite = await writeDbContext.Writes.FindAsync(id);
            if (existingWrite == null) return null;

  
            await writeDbContext.SaveChangesAsync();
            return existingWrite;
        }

        public async Task<Write?> UpdateAsync(Write write)
        {
            var existingWrite = await writeDbContext.Writes.FindAsync(write.Id);
            if (existingWrite == null) return null;

            existingWrite.Title = write.Title;
            existingWrite.Context = write.Context;
            existingWrite.Category = write.Category;

            await writeDbContext.SaveChangesAsync();  // UPDATE
            return existingWrite;
        }
    }
}
