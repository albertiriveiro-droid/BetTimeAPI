using System.ComponentModel.DataAnnotations;

namespace BetTime.Model;

public class UserUpdateDTO
{
  [StringLength(20, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 20 caracteres.")]
  public string? Username { get; set; }

  [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
  public string? Email { get; set; }

  [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
  public string? Password { get; set; }

}