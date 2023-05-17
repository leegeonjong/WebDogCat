using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace dogcat.Models.Domain
{
    [Table("Message")]
    public class Message
    {
        public long Id { get; set; }  // PK
        public bool Status { get; set; }  // 메시지 와있는지 
        public string Title { get; set; }  // 메시지 제목
        public string Context { get; set; }  // 메시지 내용
        public DateTime Time { get; set; }  // 메시지 보낸시간

       
        public User From_id { get; set; }  // 보내는사람

        public User? To_id { get; set; } // 받는사람

    }
}
