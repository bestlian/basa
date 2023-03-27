using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasaProject.Models
{
    public class MsWordList
    {
        [Key]
        [MaxLength(50)]
        public Guid WordID { get; set; } = Guid.NewGuid();
        [MaxLength(255)]
        public string Word { get; set; }
        public string Desc { get; set; }
        [MaxLength(255)]
        public string Indonesian { get; set; }
        [MaxLength(255)]
        public string English { get; set; }
        public Guid? UserIn { get; set; }
        public Guid? UserUp { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateIn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? DateUp { get; set; }

        [MaxLength(1)]
        public Boolean IsDeleted { get; set; } = false;

    }
}