namespace BasaProject.Outputs;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

public class UserResponse
{
    public Guid UserID { get; set; }
    public string Email { get; set; }
    public int? RoleID { get; set; }
    public string RoleName { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
}

public class UserRequest
{
    [Required(ErrorMessage = "Email harus diisi!")]
    [EmailAddress(ErrorMessage = "Email tidak valid!")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password harus diisi!")]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Password minimal 8 karakter dan maksimal 255 karakter", MinimumLength = 8)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Konfirmasi password harus diisi!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password tidak sama!")]
    [Display(Name = "Konfirmasi Password")]
    public string PasswordConfirmation { get; set; }

    [Required(ErrorMessage = "Role harus dipilih")]
    public int RoleID { get; set; }

    [Required(ErrorMessage = "Nama Lembaga harus diisi!")]
    [Display(Name = "Nama User")]
    public string Name { get; set; }
}