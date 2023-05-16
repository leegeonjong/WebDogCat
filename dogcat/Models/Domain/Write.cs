using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("Write")]
    public class Write
    {
        public long Id { get; set; }  // PK
        public string Title { get; set; }  // 게시글 제목
        public string Context { get; set; }  // 게시글 내용
        public DateTime Time { get; set; }  // 게시글 작성시간
        public string Kategorie { get; set; }  // 게시글 카테고리
        public string? Image { get; set; }  // 게시글 이미지

        public FreeBoard FreeBoard { get; set; } = null;  // 자유게시판 FK
        public User User { get; set; } = null;  // 유저 FK

        public ICollection<WriteImage> Images { get; set; } = new HashSet<WriteImage>();

    }
}
