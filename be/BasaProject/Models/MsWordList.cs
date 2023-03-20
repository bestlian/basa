using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasaProject.Models
{
    public class MsWordList
    {
        [Key]
        [MaxLength(50)]
        public string? WordID { get; set; }
        [MaxLength(255)]
        public string? Word { get; set; }
        public string? Desc { get; set; }
        [MaxLength(255)]
        public string? Indonesian { get; set; }
        [MaxLength(255)]
        public string? English { get; set; }
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

    }
}