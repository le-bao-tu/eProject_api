using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.DataModel
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [Column("account_id")]
        public Guid? Id { get; set; }

        [Column("userName")]
        [StringLength(50)]
        public string? UserName { get; set; }

        [Column("email")]
        [StringLength(50)]
        public string? Email { get; set; }

        [Column("phone")]
        [StringLength(50)]
        public string? Phone { get; set; }

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("birtday")]
        public DateTime? DateTime { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("state")]
        public bool Sate { get; set; } = true;

        [Column("countError")]
        public int CountError { get; set; }

        [Column("timelock")]
        public DateTime? TimeLock { get; set; }

        [Column("islock")]
        public bool IsLock { get; set; }

        [Column("tokenChange_password")]
        public string? TolenChangePassword { get; set; }
    }
}