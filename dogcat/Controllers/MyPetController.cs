using dogcat.Models.Domain;
using dogcat.Models.ViewModels;
using dogcat.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class MyPetController : Controller
    {
        private readonly IPetRepositories _petRepositories;

        public MyPetController(IPetRepositories petRepositories)
        {
            _petRepositories = petRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> MypetPage(long id)  // 내 예완동물 보러가기 버튼 클릭시
        {
            var list = await _petRepositories.GetAllAsync(id);
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var pet = await _petRepositories.GetAsync(id);
            return View(pet);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var pet = await _petRepositories.GetAsync(id);
            if (pet == null) return null;

            return View(pet);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Pet pet)
        {
            var pets = await _petRepositories.PetupdateAsync(pet);
            return RedirectToAction("Detail", new {id = pet.Id});
        }
        [HttpPost]
        [ActionName("Detail")]
        public async Task<IActionResult> Delete(long id)
        {
            var pet = await _petRepositories.PetdeleteAsync(id);
            return RedirectToAction("MypetPage", new {id = pet.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> Add(long id)
        {

            AddPetRequest pet = new AddPetRequest()
            {
                Name = (string)TempData["Name"],
                Species = (string)TempData["Species"],
                Old = (string)TempData["Old"],
                Weight = (string)TempData["Weight"],
                Image = (string)TempData["Image"],
                Userid = id,
            };
            return View(pet);
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddPetRequest pet)
        {
            pet.Validate();
            if (pet.HasError)
            {
                TempData["NameError"] = pet.ErrorName;
                TempData["SpeciesError"] = pet.ErrorSpecies;
                TempData["OldError"] = pet.ErrorOld;
                TempData["WeightError"] = pet.ErrorWeight;


                TempData["Name"] = pet.Name;
                TempData["Species"] = pet.Species;
                TempData["Old"] = pet.Old;
                TempData["Weight"] = pet.Weight;

                return RedirectToAction("Add", new {id = pet.Userid});
            }
            var pets = new Pet()
            {
                Name = pet.Name,
                Species = pet.Species,
                Old = int.Parse(pet.Old),
                Weight = int.Parse(pet.Weight),
                UserId= pet.Userid,
            };
            await _petRepositories.AddPetAsync(pets);
            return RedirectToAction("Detail", new { id = pets.Id });
        }
    }
}
