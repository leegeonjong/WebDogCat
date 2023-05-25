using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mail; // 이메일 발송을 위한 using
using System.Net; // 네트워크 프로토콜 사용을 위한 using
using dogcat.Models.Domain;

namespace dogcat.Models.ViewModels
{
    public class EmailRequest
    {
        public string Id { get; set; } //사용자가 입력한 ID 
        public string Name { get; set; } //이름
        public string Email { get; set; } //사용자가 입력한 이메일 주소 
        public string InputPassword { get; set; } //사용자가 입력한 인증번호 
        public string RealPassword { get; set; }  //우리가 보낸 인증번호

        public static Random randomNum = new Random();
        // 난수발생 객체 생성


        public void SendEmail()
        {
            // 이메일 보내는 사람의 구글 이메일 주소
            string fromEmail = "lateaksoo@gmail.com";
            // 이메일 보내는 사람의 구글 앱 비밀번호 
            string fromPassword = "hikhvuxhscwwacew";

            // 이메일 받는 사람의 이메일 주소
            string toEmail = Email;
            // 이메일 제목
            string subject = "이메일 인증번호 안내 입니다.";
            //인증번호 생성 후 저장 
            RealPassword = ((int)Math.Floor(new Random().NextDouble() * 10000000)).ToString();
            // 이메일 내용
            string body = $"인증번호 안내 : {RealPassword}";

            // 이메일 메시지 객체 생성
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress(fromEmail);
            mail.Subject = subject;
            mail.Body = body;

            // 이메일 메시지 보내기
            using (var client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.Credentials = new NetworkCredential(fromEmail, fromPassword);
                client.Send(mail);
            }

        }//end send method


        public bool CheckEmail()
        {
            if (InputPassword == null || InputPassword.Trim() != "")
            {
                if (RealPassword == InputPassword)
                {
                    // 이메일 인증 성공
                    RealPassword = null; // 인증번호 만료시키기 
                    return true;
                }
                else
                {
                    // 이메일 인증 실패
                    RealPassword = null; // 인증번호 만료시키기 
                    return false;
                }
            }
            else
            {
                //인증번호를 입력하세요 메시지 나오기
                return false;
            }
        }

    }
}
