using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_PI.Elements
{
    class List
    {
        public Board board = default(Board);
        public int id;
        public String name;

        public List(Board board, int id, String name)
        {
            this.board = board;
            this.id = id;
            this.name = name;
        }
    }
}
