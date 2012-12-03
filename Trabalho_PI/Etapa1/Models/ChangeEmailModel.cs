using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Etapa1.Models
{
    public class ChangeEmailModel
    {
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        [RegularExpression("^[a-z0-0_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Email inválido!")]
        public String OldEmail { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        [RegularExpression("^[a-z0-0_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Email inválido!")]
        public String NewEmail { get; set; }
    }
}