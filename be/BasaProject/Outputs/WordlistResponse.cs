namespace BasaProject.Outputs;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

public class WordlistResponse
{

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