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

        //public Write Writes { get; set; } = null;  // 게시글 FK
        public User User { get; set; } = null;  //  유저 FK 

        [DefaultValue(0)]
        public int View { get; set; } //게시글 조회수

    }
}
