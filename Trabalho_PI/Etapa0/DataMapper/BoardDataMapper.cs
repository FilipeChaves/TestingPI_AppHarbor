using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_PI.Elements;
namespace Trabalho_PI.DataMapper
{
    class BoardDataMapper : IDataMapper<Board>
    {
        List<Board> _boards;
        public BoardDataMapper()
        {
            _boards = new List<Board>();
        }

        public void Add(Board b)
        {
            _boards.Add(b);
        }

        public void Remove(Board b)
        {
            _boards.Remove(b);
        }

        public Board GetById(int id)
        {
            return _boards.Find(board => board.id == id);
        }

        public IEnumerable<Board> GetAll()
        {
            return _boards;
        }
    }
}
