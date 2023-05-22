using dogcat.Data;
using dogcat.Models.Domain;
using dogcat.Models.ViewModels;
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
           var list = await _dogcatDbContext.Pets.Where(s => s.UserId == id).ToListAsync();
            if (list.Count == 0) return null;
            
            return list;
        }

        public async Task<Pet> GetAsync(long id)
        {
            var pet = await _dogcatDbContext.Pets
                                .Where(s => s.Id == id)
                                .Include(u => u.PetImages)
                                .FirstOrDefaultAsync();
            return pet;
        }
        
        public async Task<Pet> AddPetAsync(Pet pet)
        {
            await _dogcatDbContext.Pets.AddAsync(pet);
            await _dogcatDbContext.SaveChangesAsync();
            return pet;
        }
        public async Task<Pet?> PetdeleteAsync(long id)
        {
            var pet = await _dogcatDbContext.Pets.FirstOrDefaultAsync(s => s.Id == id);
            _dogcatDbContext.Pets.Remove(pet);
            await _dogcatDbContext.SaveChangesAsync();
            return pet;
        }

        public async Task<Pet?> PetupdateAsync(Pet pet)
        {
            var pets = await _dogcatDbContext.Pets.FindAsync(pet.Id);
            if (pets == null) return null;
            else
            {
                pets.Name = pet.Name;
                pets.Species = pet.Species;
                pets.Old = pet.Old;
                pets.Weight = pet.Weight;
                pets.Image = pet.Image;
            }
            await _dogcatDbContext.SaveChangesAsync();
            return pets;
        }

        public async Task<PetImage?> AddimageAsync(PetImage pet) 
        {
            await _dogcatDbContext.PetImages.AddAsync(pet);
            await _dogcatDbContext.SaveChangesAsync();
            return pet;
        }
        public async Task<PetImage?> UpdateimageAsync(string fileFullPath, string savedFileName, long pets)
        {
            var pet = await _dogcatDbContext.PetImages.FirstOrDefaultAsync(x => x.PetId == pets);
            if (pets == null) return null;
            else
            {
                pet.O_image = fileFullPath;
                pet.D_image = savedFileName;
            }
            await _dogcatDbContext.SaveChangesAsync();
            return pet;
        }
    }
}
