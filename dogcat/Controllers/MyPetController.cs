using dogcat.Models.Domain;
using dogcat.Models.ViewModels;
using dogcat.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class MyPetController : Controller
    {
        private readonly IPetRepositories _petRepositories;
        public string UploadDir { get; set; }
        public MyPetController(IPetRepositories petRepositories, IWebHostEnvironment env)
        {
            _petRepositories = petRepositories;
            UploadDir = Path.Combine(env.ContentRootPath, "MyFiles");

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
            pet.RequestPath = $"/appfiles/{pet.Image}";
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
        public async Task<IActionResult> Edit(Pet pet, IList<IFormFile> uploadedFile)
        {
            if (uploadedFile == null)
            {
                await _petRepositories.PetupdateAsync(pet);
                return RedirectToAction("Detail", new { id = pet.Id });
            }
            else
            {
                pet.Image = uploadedFile[0].FileName;
                pet.RequestPath = $"/appfiles/{pet.Image}";
                await _petRepositories.PetupdateAsync(pet);
                foreach (var formFile in uploadedFile)
                {
                    if (formFile.Length > 0)
                    {
                        string savedFileName = formFile.FileName;  // 저장할 파일명
                        var fileFullPath = Path.Combine(UploadDir, savedFileName);


                        // 파일명이 이미 존재하는 경우 파일명 변경
                        // face01.png => face01(1).png => face01(2).png => ...
                        int filecnt = 1;
                        while (new FileInfo(fileFullPath).Exists)
                        {
                            var idx = formFile.FileName.LastIndexOf(".");
                            if (idx > -1)
                            {
                                var left = formFile.FileName.Substring(0, idx);
                                savedFileName = left + string.Format("({0})", filecnt++) + formFile.FileName.Substring(idx);
                            }
                            else
                            {
                                savedFileName = formFile.FileName + string.Format("({0})", filecnt++);
                            }

                            fileFullPath = Path.Combine(UploadDir, savedFileName);
                        }
                        using FileStream stream = new(fileFullPath, FileMode.Create);
                        await formFile.CopyToAsync(stream);

                        await _petRepositories.UpdateimageAsync(fileFullPath, savedFileName, pet.Id);
                    }
                }
                return RedirectToAction("Detail", new { id = pet.Id });
            }
        }
        [HttpPost]
        [ActionName("Detail")]
        public async Task<IActionResult> Delete(long id)
        {
            var pet = await _petRepositories.PetdeleteAsync(id);
            return RedirectToAction("MypetPage", new { id = pet.UserId });
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
        public async Task<IActionResult> Add(AddPetRequest pet, IList<IFormFile> uploadedFile)
        {
            if (uploadedFile.Count != 0)
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

                    return RedirectToAction("Add", new { id = pet.Userid });
                }
                var pets = new Pet()
                {
                    Name = pet.Name,
                    Species = pet.Species,
                    Old = int.Parse(pet.Old),
                    Weight = int.Parse(pet.Weight),
                    Image = uploadedFile[0].FileName,
                    UserId = pet.Userid,
                };
                await _petRepositories.AddPetAsync(pets);
                // 업로드 디렉토리 존재 확인
                DirectoryInfo di = new(UploadDir);
                // 없는 경우 디렉토리 생성
                if (di.Exists == false) di.Create();

                foreach (var formFile in uploadedFile)
                {
                    if (formFile.Length > 0)
                    {
                        string savedFileName = formFile.FileName;  // 저장할 파일명
                        var fileFullPath = Path.Combine(UploadDir, savedFileName);


                        // 파일명이 이미 존재하는 경우 파일명 변경
                        // face01.png => face01(1).png => face01(2).png => ...
                        int filecnt = 1;
                        while (new FileInfo(fileFullPath).Exists)
                        {
                            var idx = formFile.FileName.LastIndexOf(".");
                            if (idx > -1)
                            {
                                var left = formFile.FileName.Substring(0, idx);
                                savedFileName = left + string.Format("({0})", filecnt++) + formFile.FileName.Substring(idx);
                            }
                            else
                            {
                                savedFileName = formFile.FileName + string.Format("({0})", filecnt++);
                            }

                            fileFullPath = Path.Combine(UploadDir, savedFileName);
                        }
                        using FileStream stream = new(fileFullPath, FileMode.Create);
                        await formFile.CopyToAsync(stream);

                        await _petRepositories.AddimageAsync(new()
                        {
                            O_image = fileFullPath,
                            D_image = savedFileName,
                            PetId = pets.Id,
                        });
                    }
                }
                return RedirectToAction("Detail", new { id = pets.Id });
            }
            else
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

                    return RedirectToAction("Add", new { id = pet.Userid });
                }
                var pets = new Pet()
                {
                    Name = pet.Name,
                    Species = pet.Species,
                    Old = int.Parse(pet.Old),
                    Weight = int.Parse(pet.Weight),
                    UserId = pet.Userid,
                };
                await _petRepositories.AddPetAsync(pets);
                return RedirectToAction("Detail", new { id = pets.Id });
            }
        }
    }
}
