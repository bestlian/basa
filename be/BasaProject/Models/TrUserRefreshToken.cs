using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BasaProject.Models
{
    public class TrUserRefreshToken
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonIgnore]
        public Guid UserID { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }

        [MaxLength(100)]
        public string CreatedByIp { get; set; }
        [MaxLength(255)]
        public string UserAgent { get; set; }
        [MaxLength(255)]
        public string DeviceType { get; set; }

        public DateTime? Revoked { get; set; }

        [MaxLength(100)]
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
        public Guid? UserIn { get; set; }
        public Guid? UserUp { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateIn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? DateUp { get; set; }
        public int Status { get; set; } = 0;

        [MaxLength(1)]
        public Boolean? IsDeleted { get; set; } = false;
        //[NotMapped]
        [ForeignKey("UserID")]
        public virtual MsUser User { get; set; }
    }
}
