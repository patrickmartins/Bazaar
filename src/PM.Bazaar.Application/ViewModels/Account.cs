using PM.Bazaar.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace PM.Bazaar.Application.ViewModels
{
    public class AccountViewModel : ViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Avatar { get; set; }
    }

    public class UserViewModel : ViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Avatar { get; set; }
    }

    public class ChangePasswordViewModel : ViewModel
    {
        [Required(ErrorMessage = "A senha atual é obrigatória")]
        [StringLength(15, ErrorMessage = "A senha antiga deve conter no minímo 6 e no máximo 15 caracteres", MinimumLength = 6)]
        public string CurrentPassword { get; set; }

        [StringLength(15, ErrorMessage = "A senha deve conter no minímo 6 e no máximo 15 caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "A senha e a confirmação não correspondem")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel : ViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
        [StringLength(100, ErrorMessage = "O e-mail informado não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(15, ErrorMessage = "A senha deve conter no minímo 6 e no máximo 15 caracteres", MinimumLength = 6)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }

    public class RegisterAccountViewModel : ViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(15, ErrorMessage = "O nome deve conter no minímo 5 e no máximo 15 caracteres", MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [StringLength(30, ErrorMessage = "O sobrenome deve conter no minímo 5 e no máximo 30 caracteres", MinimumLength = 5)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
        [StringLength(100, ErrorMessage = "O e-mail informado não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(15, ErrorMessage = "A senha deve conter no minímo 6 e no máximo 15 caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "A senha e a confirmação não correspondem")]
        public string ConfirmPassword { get; set; }

        public RegisterImageViewModel Avatar { get; set; }
    }
}