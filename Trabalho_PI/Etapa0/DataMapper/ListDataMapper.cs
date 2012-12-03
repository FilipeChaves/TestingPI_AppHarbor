using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_PI.Elements;

namespace Trabalho_PI.DataMapper
{
    class ListDataMapper : IDataMapper<List>
    {
        List<List> _lists;

        public ListDataMapper()
        {
            _lists = new List<List>();
        }

        public void SetList(List<List> list)
        {
            _lists = list;
        }

        public void Add(List l)
        {
            _lists.Add(l);
        }

        public void Remove(List l)
        {
            _lists.Remove(l);
        }

        public List GetById(int id)
        {
            return _lists.Find(list => list.id == id);
        }

        public IEnumerable<List> GetAllByBoard(Board board)
        {
            return _lists.FindAll(list => list.board.id == board.id);
        }

        public IEnumerable<List> GetAll()
        {
            return _lists;
        }
    }
}
