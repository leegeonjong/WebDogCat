using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("Comment")]
    public class Comment
    {
        public long Id { get; set; }  // PK //주석 추가
        public string Content { get; set; }  // 댓글 내용
        public DateTime Time { get; set; }  // 댓글 작성시간


        public string  NickName { get; set; }
        public User User { get; set; }  //  유저 FK 
        public long UserId { get; set; }

        public Write Write { get; set; }
        public long WriteId { get; set; }
    }
}
