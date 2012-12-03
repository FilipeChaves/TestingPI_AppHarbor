using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etapa1.Models;

namespace Etapa1.DataMappers
{
    class CardDataMapper : IDataMapper<CardsModel, int>
    {
        List<CardsModel> _cards;
        private static CardDataMapper _cardDataMapper;
        private int _nextCid;

        private CardDataMapper()
        {
            var lm = ListDataMapper.GetListDataMapper();
            _cards = new List<CardsModel>();
            var card = new CardsModel { Id = 1, List = lm.GetById(1), Name = "Data Mappers", InitialDate = new DateTime(), DueDate = new DateTime(), Idx = 1 };
            _cards.Add(card);
            card = new CardsModel { Id = 2, List = lm.GetById(2), Name = "Etapa0", InitialDate = new DateTime(), DueDate = new DateTime(), Idx = 1 };
            _cards.Add(card);
            _nextCid = 3;
        }

        public void SetList(List<CardsModel> list)
        {
            _cards = list;
        }

        public static CardDataMapper GetCardDataMapper()
        {
            return _cardDataMapper ??(_cardDataMapper = new CardDataMapper());
        }

        public void Add(CardsModel c)
        {
            c.Id = _nextCid++;
            c.Idx = GetAllByList(c.List).Count() + 1;
            _cards.Add(c);
        }

        public void Remove(CardsModel c)
        {
            _cards.Remove(c);
        }

        public CardsModel GetById(int id)
        {
            return _cards.Find(card => card.Id == id);
        }

        public IEnumerable<CardsModel> GetAllByList(ListsModel list)
        {
            return _cards.FindAll(card => card.List != null && list.Id == card.List.Id).OrderBy(card => card.Idx);
        }

        public IEnumerable<CardsModel> GetAll()
        {
            return _cards;
        }
    }
}
