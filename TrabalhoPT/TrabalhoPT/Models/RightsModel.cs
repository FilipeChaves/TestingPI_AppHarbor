using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoPT.Models
{
    public class RightsModel
    {
        [Required]
        [DisplayName("Username")]
        public string Name { get; set; }
        [Required]
        public int BoardId { get; set; }
    }
}