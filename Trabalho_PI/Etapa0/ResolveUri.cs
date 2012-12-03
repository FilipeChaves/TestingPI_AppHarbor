using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_PI.Elements;

namespace Trabalho_PI
{
    static class ResolveUri
    {
        public static string ForBoards()
        {
            return "http://localhost:8080/boards";
        }

        public static string ForBoard(Board b)
        {
            return string.Format("http://localhost:8080/boards/{0}/lists", b.id);
        }

        public static string ForList(List l)
        {
            return string.Format("http://localhost:8080/boards/{0}/lists/{1}/cards", l.board.id, l.id);
        }

        public static string ForCard(Card c)
        {
            return string.Format("http://localhost:8080/boards/{0}/lists/{1}/cards/{2}", c.list.board.id, c.list.id, c.id);
        }

        public static string ForCardWithoutList(Card c)
        {
            return string.Format("http://localhost:8080/cards/{0}", c.id);
        }

        public static string ForListToRemove(List l)
        {
            return string.Format("http://localhost:8080/boards/{0}/lists/{1}/toremove", l.board.id, l.id);
        }
    }
}
