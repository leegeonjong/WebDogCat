using Azure.Core;
using dogcat.Data;
using dogcat.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dogcat.Repositories
{
    public class WriteRepository : IWriteRepository
    {
        private readonly DogcatDbContext writeDbContext;

        public WriteRepository(DogcatDbContext writeDbContext)
        {
            Console.WriteLine("WriteRepoitory() 생성");
            this.writeDbContext = writeDbContext;
                   }
        public async Task<Write> AddAsync(Write write)
        {
            await writeDbContext.Writes.AddAsync(write);
            await writeDbContext.SaveChangesAsync();
            return write;
        }

        public async Task<long> CountAsync()
        {
            return await writeDbContext.Writes.CountAsync();
        }


        public async Task<Write?> DeleteAsync(long id)
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


        //카테고리
    

        public async Task<IEnumerable<Write>> GetFromRowAsync(int fromRow, int pageRows, string category)
        {
            if (category == "전체" || category == null)
            {
                return await writeDbContext.Writes
                    .OrderByDescending(x => x.Id)  // 최신순으로
                    .Skip(fromRow)    // fromRow 번째 부터
                    .Take(pageRows)   // pageRows 개를 SELECT
                    .ToListAsync();
            }
            else
            {
                return await writeDbContext.Writes
               .Where(x => x.Category == category)  // 카테고리가 일치하는 것만 선택
               .OrderByDescending(x => x.Id)  // 최신순으로
               .Take(pageRows)   // pageRows 개를 SELECT
               .ToListAsync();
            }
        }

        public async Task<Write?> IncViewCntAsync(long id)
        {
            var existingWrite = await writeDbContext.Writes.FindAsync(id);
            if (existingWrite == null) return null;
            //
            existingWrite.ViewCnt++;
            await writeDbContext.SaveChangesAsync();
            return existingWrite;
        }

        public async Task<Write?> UpdateAsync(Write write)
        {
            var existingWrite = await writeDbContext.Writes.FindAsync(write.Id);
            if (existingWrite == null) return null;

            existingWrite.Title = write.Title;
            existingWrite.Category = write.Category;
            existingWrite.Context = write.Context;
            existingWrite.Image = write.Image;

            await writeDbContext.SaveChangesAsync();  // UPDATE
            return existingWrite;
        }

        public async Task<WriteImage?> AddimageAsync(WriteImage write)
        {
            await writeDbContext.Images.AddAsync(write);
            await writeDbContext.SaveChangesAsync();
            return write;
        }
        public async Task<WriteImage?> UpdateimageAsync(string fileFullPath, string savedFileName, long id)
        {
            var write = await writeDbContext.Images.FirstOrDefaultAsync(x => x.WriteId == id);
            if (write == null) return null;
            write.O_image = fileFullPath;
            write.D_image = savedFileName;
            return write;
        }
    }
}
