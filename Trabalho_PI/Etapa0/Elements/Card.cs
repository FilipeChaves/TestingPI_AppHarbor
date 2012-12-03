using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_PI.Elements
{
    class Card
    {
        public List list = default(List);
        public int id;
        public String name;
        public String description;
        public DateTime creationDate;
        public DateTime dueDate;
        public int idx;

        public Card(List list, int id, String name, String description, DateTime creationDate, int idx)
        {
            this.list = list;
            this.id = id;
            this.name = name;
            this.description = description;
            this.creationDate = creationDate;
            this.idx = idx;
        }

        public Card(List list, int id, String name, String description, DateTime creationDate, DateTime dueDate, int idx)
        {
            this.list = list;
            this.id = id;
            this.name = name;
            this.description = description;
            this.creationDate = creationDate;
            this.dueDate = dueDate;
            this.idx = idx;
        }
    }
}
