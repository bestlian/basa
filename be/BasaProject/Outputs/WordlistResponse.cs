namespace BasaProject.Outputs;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

public class WordlistResponse
{
    public Guid WordID { get; set; }
    public string Word { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Indonesian { get; set; }
    public string English { get; set; }
}

public class WordlistRequest
{
    [MaxLength(255)]
    [Required]
    public string Word { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Type { get; set; }
    [MaxLength(255)]
    public string Indonesian { get; set; }
    [MaxLength(255)]
    public string English { get; set; }
}