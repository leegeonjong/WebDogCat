using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("Write")]
    public class Write
    {
        public long Id { get; set; }  // PK
        public string NickName { get; set; }  // NickName
        public string Title { get; set; }  // 게시글 제목
        public string Context { get; set; }  // 게시글 내용
        public DateTime Time { get; set; }  // 게시글 작성시간
        public string Category { get; set; }  // 게시글 카테고리
        public string? Image { get; set; }  // 게시글 이미지

        
        public long UserId { get; set; } //작성자 Id
        public int ViewCnt { get; set; } //게시글 조회수
        public User User { get; set; } = null;  // 유저 FK

        public ICollection<WriteImage> Images { get; set; } = new HashSet<WriteImage>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        [NotMapped]
        public string? RequestPath { get; set; }  // 저장된 파일에 대한 요청 경로 (url)

    }
}
