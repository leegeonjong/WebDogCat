using dogcat.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace dogcat.Models.ViewModels
{
    public class AddPetRequest
    {
        public long Id { get; set; }  // PK
        public string Species { get; set; }  // 펫 종
        public string Old { get; set; }  // 펫 생년월일
        public string Name { get; set; }  // 펫 이름
        public string Weight { get; set; }  // 펫 무게
        public string? Image { get; set; }  // 펫 이미지
        public long Userid { get; set; }  // 유저 uid

       

        public bool HasError { get; set; } = false;
        public string? ErrorName { get; set; }
        public string? ErrorSpecies { get; set; }
        public string? ErrorOld { get; set; }
        public string? ErrorWeight { get; set; }
        public void Validate()
        {
            if (Name.IsNullOrEmpty())
            {
                ErrorName = "이름을 입력해주세요.";
                HasError = true;
            }
            if (Species.IsNullOrEmpty())
            {
                ErrorSpecies = "종류를 선택해주세요.";
                HasError = true;
            }
            if (Old.IsNullOrEmpty())
            {
                ErrorOld = "나이를 입력해주세요.";
                HasError = true;
            }
            else if (int.Parse(Old) < 0)
            {
                ErrorOld = "다시 확인후 입력해주세요.";
                HasError = true;
            }
            if (Weight.IsNullOrEmpty())
            {
                ErrorWeight = "무게를 입력해주세요.";
                HasError = true;
            }
            else if (int.Parse(Weight) < 0)
            {
                ErrorWeight = "다시 확인후 입력해주세요.";
                HasError = true;
            }
        }
    }
}
