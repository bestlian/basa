namespace BasaProject.Outputs;

using System.Text.Json.Serialization;

public class UserResponse
{
    public string? UserID { get; set; }
    public string? Email { get; set; }
    public int? RoleID { get; set; }
    public string? RoleName { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public string? Password { get; set; }
}