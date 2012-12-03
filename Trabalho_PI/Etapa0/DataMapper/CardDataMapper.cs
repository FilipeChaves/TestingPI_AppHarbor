using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_PI.Elements;

namespace Trabalho_PI.DataMapper
{
    class CardDataMapper : IDataMapper<Card>
    {
        List<Card> _cards;
        public CardDataMapper()
        {
            _cards = new List<Card>();
        }

        public void SetList(List<Card> list)
        {
            _cards = list;
        }

        public void Add(Card c)
        {
            _cards.Add(c);
        }

        public void Remove(Card c)
        {
            _cards.Remove(c);
        }

        public Card GetById(int id)
        {
            return _cards.Find(card => card.id == id);
        }

        public IEnumerable<Card> GetAllByList(List list)
        {
            return _cards.FindAll(card => list.Equals(card.list)).OrderBy(card => card.idx);
        }

        public IEnumerable<Card> GetAll()
        {
            return _cards;
        }
    }
}
