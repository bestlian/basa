using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasaProject.Models
{
    public class MsUser
    {
        [Key]
        [MaxLength(50)]
        public string? UserID { get; set; }
        [MaxLength(255)]
        public string? Email { get; set; }
        [MaxLength(255)]
        public string? Password { get; set; }
        public int? RoleID { get; set; }
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? UserIn { get; set; }
        [MaxLength(50)]
        public string? UserUp { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateIn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? DateUp { get; set; }

        [MaxLength(1)]
        public Boolean IsDeleted { get; set; } = false;

        [ForeignKey("RoleID")]
        public virtual MsRole? Role { get; set; }
    }
}