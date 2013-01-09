using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoPT.Models
{
    public class BoardsModel
    {
        //  [Required]
        public int Id { get; set; } //Preenchido automaticamente
        [Required]
        [DisplayName("Nome")]
        public String Name { get; set; }
        [DisplayName("Descrição")]
        public String Description { get; set; }
    }
}