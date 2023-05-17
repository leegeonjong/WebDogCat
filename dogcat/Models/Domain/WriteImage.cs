using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("Image")]
    public class WriteImage
    {
        public long Id { get; set; }  // PK
        public string O_image { get; set; }  // 이미지 원본명
        public string D_image { get; set; }  // 이미지 저장명

        //public ICollection<Write> Writes { get; set; } = new HashSet<Write>();
        public Write Write { get; set; }
        public long WriteId { get; set; }
    }
}
