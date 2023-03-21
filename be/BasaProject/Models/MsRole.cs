using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasaProject.Models
{
    public class MsRole
    {
        [Key]
        public int RoleID { get; set; }

        [Display(Name = "Role Name")]
        [MaxLength(255)]
        public string? RoleName { get; set; }
        public Guid? UserIn { get; set; }
        public Guid? UserUp { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateIn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? DateUp { get; set; }

        [MaxLength(1)]
        public Boolean IsDeleted { get; set; } = false;
        public ICollection<MsUser>? Users { get; set; }
    }
}
