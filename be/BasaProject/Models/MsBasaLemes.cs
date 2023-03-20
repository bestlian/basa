using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasaProject.Models
{
    public class MsBasaLemes
    {
        [Key]
        [MaxLength(50)]
        public string? ID { get; set; }
        [MaxLength(50)]
        public string? FirstWord { get; set; }
        [MaxLength(255)]
        public string? SecondWord { get; set; }
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

        [ForeignKey("FirstWord")]
        public virtual MsWordList? Word { get; set; }
    }
}