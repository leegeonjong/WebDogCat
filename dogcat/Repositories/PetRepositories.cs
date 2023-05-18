using dogcat.Data;
using dogcat.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dogcat.Repositories
{
    public class PetRepositories : IPetRepositories
    {
        private readonly DogcatDbContext _dogcatDbContext;
        public PetRepositories(DogcatDbContext dogcatDbContext)
        {
            _dogcatDbContext = dogcatDbContext;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync(long id)
        {
           var list = await _dogcatDbContext.Pets.ToListAsync();

            return list;
        }

        public async Task<Pet> GetAsync(long id)
        {
            var pet = await _dogcatDbContext.Pets.FirstOrDefaultAsync(s => s.Id == id);
            return pet;
        }

        public Task<Pet> Petdelete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Pet?> Petupdate(Pet pet)
        {
            throw new NotImplementedException();
        }
    }
}
