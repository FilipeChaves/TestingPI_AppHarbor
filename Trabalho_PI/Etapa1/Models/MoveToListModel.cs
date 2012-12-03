using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Etapa1.Models
{
    public class MoveToListModel
    {
        [Required]
        [DisplayName("Id da nova lista")]
        public int ListId { get; set; }
        [Required]
        [DisplayName("Id do cartao a mover")]
        public int CardId { get; set; }
    }
}