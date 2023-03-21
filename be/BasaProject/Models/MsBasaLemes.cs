using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasaProject.Models
{
    public class MsBasaLemes
    {
        [Key]
        [MaxLength(50)]
        public Guid ID { get; set; } = Guid.NewGuid();
        [MaxLength(50)]
        public Guid FirstWord { get; set; }
        [MaxLength(255)]
        public string? SecondWord { get; set; }
        public Guid? UserIn { get; set; }
        public Guid? UserUp { get; set; }

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