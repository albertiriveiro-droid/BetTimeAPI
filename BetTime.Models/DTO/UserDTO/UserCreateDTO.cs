using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;

public class UserCreateDTO
{   
    [Required]
    public string? Username { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
    public string? Email { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Password { get; set; }
   
  

}