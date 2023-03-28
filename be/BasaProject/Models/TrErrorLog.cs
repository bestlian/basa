using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasaProject.Models
{
    public class TrErrorLog
    {
        [Key]
        public Guid ErrorID { get; set; } = Guid.NewGuid();
        [DataType(DataType.Text)]
        public string Source { get; set; }
        [DataType(DataType.Text)]
        public string Message { get; set; }
        [DataType(DataType.Text)]
        public string InnerException { get; set; }
        [DataType(DataType.Text)]
        public string StackTrace { get; set; }

        public Guid UserIn { get; set; }
        public Guid UserUp { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateIn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? DateUp { get; set; }
        [MaxLength(1)]
        public Boolean? IsDeleted { get; set; } = false;

        [ForeignKey("UserIn")]
        public MsUser User { get; set; }
    }
}
