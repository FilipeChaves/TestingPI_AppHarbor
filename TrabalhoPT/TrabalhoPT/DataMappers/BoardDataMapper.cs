using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etapa1.Models;
namespace Etapa1.DataMappers
{
    class BoardDataMapper : IDataMapper<BoardsModel, int>
    {
        List<BoardsModel> _boards;
        private static BoardDataMapper _boardDataMapper;
        private int _nextBid;

        private BoardDataMapper()
        {
            _boards = new List<BoardsModel>();
            var bm = new BoardsModel { Description = "Board de PI - 1112 - Semestre Verao", Id = 1, Name = "PI - 1112" };
            _boards.Add(bm);
            bm = new BoardsModel { Description = "Board de PI - 1213 - Semestre Inverno" , Id = 2, Name = "PI - 1213"};
            _boards.Add(bm);
            _nextBid = 3;
        }

        public void Add(BoardsModel b)
        {
            b.Id = _nextBid++;
            _boards.Add(b);
        }

        public static BoardDataMapper GetBoardDataMapper()
        {
            return _boardDataMapper??(_boardDataMapper = new BoardDataMapper());
        }

        public void Remove(BoardsModel b)
        {
            _boards.Remove(b);
        }

        public BoardsModel GetById(int id)
        {
            return _boards.Find(board => board.Id == id);
        }

        public IEnumerable<BoardsModel> GetBoardsFrom(AccountModel account)
        {
            return _boards.FindAll(board => account.CanReadBoard(board.Id));
        }

        public BoardsModel GetByName(String name)
        {
            return _boards.Find(board => board.Name == name);
        }

        public IEnumerable<BoardsModel> GetAll()
        {
            return _boards;
        }
    }
}
