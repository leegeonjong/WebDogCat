using System.Security.Principal;

namespace dogcat.Models.ViewModels
{
   
    public class AddUserRequest
    {
        private int _default = 0;
        public long Id { get; set; }  // PK
        public string Userid { get; set; }  // 유저 id
        public string Pw { get; set; }  // 유저 pw
        public string Name { get; set; }  // 유저 이름
        public string NickName { get; set; }  // 유저 닉네임
        public string PhoneNum { get; set; }  // 유저 전화번호
        public string Mail { get; set; }  // 유저 이메일

        public int Admin // 관리자 여부 기본값 : 0
        {
            get => _default;
            set { _default = 0; }
        }

        public int Ban // 벤 여부 기본값 : 0
        {
            get => _default;
            set { _default = 0; }
        }



        //Validation
        public bool HasError { get; set; } = false;  // 검증오류존재여부
        public string? ErrorId { get; set; }
        public string? ErrorPw { get; set; }
        public string? ErrorName { get; set; }
        public string? ErrorNickName { get; set; }
        public string? ErrorPhoneNum { get; set; }
        public string? ErrorMail { get; set; }
    }
}
