using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("Write")]
    public class Write
    {
        public long Id { get; set; }  // PK
        public long UserId { get; set; }  // 게시글 작성자 uid
        public string Title { get; set; }  // 게시글 제목
        public string Context { get; set; }  // 게시글 내용
        public DateTime Time { get; set; }  // 게시글 작성시간
        public string Kategorie { get; set; }  // 게시글 카테고리
        public string? Image { get; set; }  // 게시글 이미지

        public FreeBoard FreeBoard { get; set; } = null;  // 자유게시판 FK
        public AdminBoard AdminBoad { get; set; } = null;  // 공지사항 FK

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();

    }
}
