using dogcat.Models.Domain;
using dogcat.Models.ViewModels;

namespace dogcat.Repositories
{
    public interface IPetRepositories
    {
        Task<IEnumerable<Pet>> GetAllAsync(long id);  // 유저 uid로 유저의 펫 정보 모두 가져오기

        Task<Pet> GetAsync(long id);  // 펫 uid로 특정 펫의 정보 가져오기

        Task<Pet?> PetupdateAsync(Pet pet);  // 펫 정보 수정

        Task<Pet?> PetdeleteAsync(long id);  // 펫 정보 삭제

        Task<Pet> AddPetAsync(Pet pet);  // 펫 등록하기

        Task<PetImage?> AddimageAsync(PetImage pet);
    }
}
