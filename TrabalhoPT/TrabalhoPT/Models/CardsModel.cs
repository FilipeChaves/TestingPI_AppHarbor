using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Etapa1.Models
{
    public class CardsModel
    {
        public int Id { get; set; }
        public ListsModel List { get; set; }
        [Required]
        [DisplayName("Nome")]
        public String Name { get; set; }
        [DisplayName("Descrição")]
        public String Description { get; set; }
        [Required]
        [DisplayName("Data de Inicio")]
        public DateTime InitialDate { get; set; }
        [DisplayName("Data de Fim")]
        public DateTime DueDate { get; set; }
        public int Idx { get; set; }
    }
}