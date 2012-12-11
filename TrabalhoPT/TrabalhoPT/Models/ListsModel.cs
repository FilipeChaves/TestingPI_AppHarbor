using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Etapa1.Models
{
    public class ListsModel
    {
        
        public int Id { get; set; } //Preenchido automaticamente
        public BoardsModel Board { get; set; }
        [Required]
        [DisplayName("Nome")]
        public String Name { get; set; }
    }
}