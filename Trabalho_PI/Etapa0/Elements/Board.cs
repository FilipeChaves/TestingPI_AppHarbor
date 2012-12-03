using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_PI.Elements
{
    class Board
    {
        public int id;
        public String name;
        public String description;

        public Board(int id, String name, String description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
    }
}
