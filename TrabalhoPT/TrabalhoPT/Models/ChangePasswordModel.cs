using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Etapa1.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        public String OldPw { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        [Compare("Pw2", ErrorMessage = "As passwords devem coincidir!")]
        [DataType(DataType.Password)]
        public String Pw1 { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        [Compare("Pw1", ErrorMessage = "As passwords devem coincidir!")]
        [DataType(DataType.Password)]
        public String Pw2 { get; set; }
    }
}