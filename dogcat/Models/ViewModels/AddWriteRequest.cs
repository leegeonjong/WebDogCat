using dogcat.Models.Domain;
using Microsoft.IdentityModel.Tokens;

namespace dogcat.Models.ViewModels
{
    // '글 작성' 시 view 로부터 넘어오는 데이터
    public class AddWriteRequest
    {
        public string Title { get; set; }
        public User user { get; set; }
        public string? Context { get; set; }
        public string category { get; set; }

        public DateTime Time { get; set; }
        // Validation
        public bool HasError { get; set; } = false;  // 검증오류존재여부
        public string? ErrorName { get; set; }   // '작성자' 관련 검증 오류메세지
        public string? ErrorSubject { get; set; } // '제목' 관련 검증 오류메세지

       
    }
}
