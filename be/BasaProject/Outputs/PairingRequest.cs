using System.ComponentModel.DataAnnotations;

namespace BasaProject.Outputs
{
    public class PairingRequest
    {
        [Required]
        public Guid WordID { get; set; }
        [Required]
        public string BadWord { get; set; }
    }
}