using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("PetImage")]
    public class PetImage
    {
        public long Id { get; set; }
        public string O_image { get; set; }  // 펫 이미지 원본명
        public string D_image { get; set; }  // 펫 이미지 저장명

        public Pet Pet { get; set; }  // Pet FK
        public long PetId { get; set; }
    }
}
