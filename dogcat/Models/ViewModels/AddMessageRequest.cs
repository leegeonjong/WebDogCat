using Microsoft.IdentityModel.Tokens;

namespace dogcat.Models.ViewModels
{
    public class AddMessageRequest
    {
        public string Title { get; set; }  // 메시지 제목
        public string Context { get; set; }  // 메시지 내용
        public DateTime Time { get; set; }  // 발신 시간
        public string Mail { get; set; }  // 받는 사람 
        public long From_id { get; set; }  // 보내는 사람

        public bool HasError { get; set; } = false;
        public string? ErrorTitle { get; set; }
        public string? ErrorContext { get; set; }
        public string? ErrorMail { get; set; }

        public void Validate()
        {
            if(Title.IsNullOrEmpty())
            {
                ErrorTitle = "제목을 입력해주세요";
                HasError = true;
            }
            if(Context.IsNullOrEmpty())
            {
                ErrorContext = "내용을 입력해주세요";
                HasError = true;
            }
            if (Mail.IsNullOrEmpty())
            {
                ErrorMail = "받는사람의 입력해주세요";
                HasError = true;
            }
        }
    }
}
