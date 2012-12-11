using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etapa1.Models;

namespace Etapa1.DataMappers
{
    class ListDataMapper : IDataMapper<ListsModel, int>
    {
        List<ListsModel> _lists;
        private static ListDataMapper _listDataMapper;
        private int _nextId;

        private ListDataMapper()
        {
            BoardDataMapper bm = BoardDataMapper.GetBoardDataMapper();
            _lists = new List<ListsModel>();
            _nextId = 1;
            var list = new ListsModel { Id = _nextId++, Name = "To Do", Board = bm.GetById(2) };
            _lists.Add(list);
            list = new ListsModel { Id = _nextId++, Name = "Done", Board = bm.GetById(2) };
            _lists.Add(list);
        }

        public static ListDataMapper GetListDataMapper()
        {
            return _listDataMapper??(_listDataMapper = new ListDataMapper());
        }

        public void SetList(List<ListsModel> list)
        {
            _lists = list;
        }

        public void Add(ListsModel l)
        {
            l.Id = _nextId++;
            _lists.Add(l);
        }

        public void Remove(ListsModel l)
        {
            _lists.Remove(l);
        }

        public ListsModel GetById(int id)
        {
            return _lists.Find(list => list.Id == id);
        }

        public ListsModel GetByName(String name)
        {
            return _lists.Find(list => list.Name == name);
        }

        public IEnumerable<ListsModel> GetAllByBoard(BoardsModel board)
        {
            return _lists.FindAll(list => list.Board.Id == board.Id);
        }

        public IEnumerable<ListsModel> GetAll()
        {
            return _lists;
        }
    }
}
