using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoPT.Models
{
    public class ListsModel : BLCModel
    {
        public BoardsModel Board { get; set; }
    }
}