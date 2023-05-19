using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("EmailVerify")]
    public class EmailVerify
    {
        public string Email { get; set; } // 이메일 
        public string InputPassword { get; set; } // 인증번호
        public string RealPassword { get; set; }
    
}
}
