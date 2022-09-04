using System.ComponentModel.DataAnnotations;

namespace YMsg.Models.RequestModels;

public class RegisterRequest
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}