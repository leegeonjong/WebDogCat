using Microsoft.IdentityModel.Tokens;

namespace dogcat.Models.ViewModels
{
    public class addWriteRequest
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string NickName { get; set; }
        public string Category { get; set; }
        public string? Context { get; set; }
        public string? Image { get; set; }

        // Validation
        public bool HasError { get; set; } = false;  // 검증오류존재여부
        public string? ErrorTitle { get; set; }   // '작성자' 관련 검증 오류메세지
        public string? ErrorSubject { get; set; } // '제목' 관련 검증 오류메세지

        //public void Validate()
        //{
        //    if (UserId == null)
        //    {
        //        ErrorName = "작성자는 필수입니다";
        //        HasError = true;
        //    }

        //    if (Title.IsNullOrEmpty())
        //    {
        //        ErrorTitle = "제목은 필수입니다";
        //        HasError = true;
        //    }
        //    else if (Title.Length < 4)
        //    {
        //        ErrorTitle = "제목은 4글자 이상이어야 합니다";
        //        HasError = true;
        //    }

        //}
    }
}