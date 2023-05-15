using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("AdminBoard")]
    public class AdminBoard
    {
        public long Id { get; set; }  // PK
        public User User { get; set; } = null;  // user FK
        
    }
}
