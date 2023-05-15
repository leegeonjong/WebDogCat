using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("FreeBoard")]
    public class FreeBoard
    {
        public long Id { get; set; }  // PK
        public User User { get; set; } = null;  // 유저 FK

        
        

    }
}
