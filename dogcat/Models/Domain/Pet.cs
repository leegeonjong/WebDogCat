using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("Pet")]
    public class Pet
    {
        public long Id { get; set; }  // PK
        public string Species { get; set; }  // 펫 종
        public DateTime Old { get; set; }  // 펫 생년월일
        public string Name { get; set; }  // 펫 이름
        public int Weight { get; set; }  // 펫 무게
        public string? Image { get; set; }  // 펫 이미지

        public User User { get; set; }  // 유저 FK
    }
}
