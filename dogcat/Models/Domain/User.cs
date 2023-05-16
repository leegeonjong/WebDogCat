using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace dogcat.Models.Domain
{
    [Table("User")]
    public class User
    {
        public long Id { get; set; }  // PK
        public string Userid { get; set; }  // 유저 id
        public string Pw { get; set; }  // 유저 pw
        public string Name { get; set; }  // 유저 이름
        public string NickName { get; set; }  // 유저 닉네임
        public string PhoneNum { get; set; }  // 유저 전화번호
        public string Mail { get; set; }  // 유저 이메일
        [DefaultValue(false)]
        public bool Admin { get; set; }  // 관리자 권한
        public int Ban { get; set; }  // 유저 계정 정지

        public ICollection<Write> Writes { get; set; } = new HashSet<Write>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();





    }
}
