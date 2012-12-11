using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Etapa1.Models
{
    public class RegisterAccountModel
    {
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        public String Username { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        [RegularExpression("^[a-z0-0_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Email inválido!")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "As passwords devem coincidir!")]
        public String Password { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As passwords devem coincidir!")]
        public String ConfirmPassword { get; set; }
    }
}