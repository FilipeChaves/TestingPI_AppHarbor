using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Trabalho_PI.DataMapper;
using Trabalho_PI.Elements;
using Trabalho_PI.Views;
using WebGarten2;
using WebGarten2.Html;

namespace Trabalho_PI
{
    class TrelloController
    {
        private static BoardDataMapper boards;
        private static int nextBID = 1;
        private static ListDataMapper lists;
        private static int nextLID = 1;
        private static CardDataMapper cards;
        private static int nextCID = 1;

        public TrelloController()
        {
            if (boards == null)
            {
                boards = new BoardDataMapper();
                lists = new ListDataMapper();
                cards = new CardDataMapper();
                boards.Add(new Board(nextBID++, "ISEL", "Quadro sobre trabalhos do ISEL"));
                lists.Add(new List(boards.GetById(1), nextLID++, "A realizar"));
                cards.Add(new Card(lists.GetById(1), nextCID++, "Trabalho de PI", "Etapa 1", DateTime.Now, 1));
            }
        }

        /* * * * *
         *  GET  *
         * * * * */

        [HttpMethod("GET", "/boards")]
        public HttpResponseMessage GetBoards()
        {
            return Get();
        }

        [HttpMethod("GET", "/boards/")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
                {
                    Content = new BoardsView(boards.GetAll()).AsHtmlContent()
                };
        }

        [HttpMethod("GET", "/boards/{bid}/lists")]
        public HttpResponseMessage GetLists(int bid)
        {
            return Get(bid);
        }

        [HttpMethod("GET", "/boards/{bid}/lists/")]
        public HttpResponseMessage Get(int bid)
        {
            if (bid >= nextBID)
                return NotFound();
            var board = boards.GetById(bid);
            return new HttpResponseMessage
            {
                Content = new ListsView(lists.GetAllByBoard(board), board).AsHtmlContent()
            };
        }
        [HttpMethod("GET", "/boards/{bid}/lists/{lid}/cards")]
        public HttpResponseMessage GetCards(int bid, int lid)
        {
            return Get(bid, lid);
        }

        [HttpMethod("GET", "/boards/{bid}/lists/{lid}/cards/")]
        public HttpResponseMessage Get(int bid, int lid)
        {
            if (bid > nextBID || lid > nextLID)
                return NotFound();
            var list = lists.GetById(lid);
            return new HttpResponseMessage
            {
                Content = new CardsView(cards.GetAllByList(list), list).AsHtmlContent()
            };
        }

        [HttpMethod("GET", "/boards/{bid}/lists/{lid}/cards/{cid}")]
        public HttpResponseMessage GetCard(int bid, int lid, int cid)
        {
            return Get(bid, lid, cid);
        }

        [HttpMethod("GET", "/cards/{cid}")]
        public HttpResponseMessage GetCardAux(int cid)
        {
            return GetCard(cid);
        }

        [HttpMethod("GET", "/cards/{cid}/")]
        public HttpResponseMessage GetCard(int cid)
        {
            if (cid >= nextCID)
                return NotFound();
            return new HttpResponseMessage
            {
                Content = new CardArchiveView(cards.GetById(cid)).AsHtmlContent()
            };
        }

        [HttpMethod("GET", "/boards/{bid}/lists/{lid}/cards/{cid}/")]
        public HttpResponseMessage Get(int bid, int lid, int cid)
        {
            if (bid > nextBID || lid > nextLID || cid >= nextCID)
                return NotFound();
            return new HttpResponseMessage
            {
                Content = new CardView(cards.GetById(cid)).AsHtmlContent()
            };
        }

        /* * * * *
         *  POST *
         * * * * */

        [HttpMethod("POST", "/boards")]
        public HttpResponseMessage Post(NameValueCollection content)
        {
            
            var descr = content["descr"];
            var name = content["name"];
            if (descr == "" || name == "")
            {
                return BadRequest();
            }
            boards.Add(new Board(nextBID++, name, descr));
            var resp = new HttpResponseMessage(HttpStatusCode.SeeOther);
            resp.Headers.Location = new Uri(ResolveUri.ForBoards());
            return resp;
        }

        [HttpMethod("POST", "/cards/{cid}")]
        public HttpResponseMessage PostToArchiveAux(int cid)
        {
            return PostToArchive(cid);
        }

        [HttpMethod("POST", "/cards/{cid}/")]
        public HttpResponseMessage PostToArchive(int cid)
        {
            var card = cards.GetById(cid);
            card.list = null;
            var resp = new HttpResponseMessage(HttpStatusCode.SeeOther);
            resp.Headers.Location = new Uri(ResolveUri.ForCardWithoutList(card));
            return resp;
        }

        [HttpMethod("POST", "/boards/{bid}/lists")]
        public HttpResponseMessage Post(NameValueCollection content, int bid)
        {
            var name = content["name"];
            if (name == "")
            {
                return BadRequest();
            }
            var board = boards.GetById(bid);
            lists.Add(new List(board, nextLID++, name));
            var resp = new HttpResponseMessage(HttpStatusCode.SeeOther);
            resp.Headers.Location = new Uri(ResolveUri.ForBoard(board));
            return resp;
        }

        [HttpMethod("POST", "/boards/{bid}/lists/{lid}/cards")]
        public HttpResponseMessage Post(NameValueCollection content, int bid, int lid)
        {
            var name = content["name"];
            var descr = content["descr"];
            var initialDate = content["date"];
            var dueDate = content["dueDate"];
            var idx = content["idx"];

            if (name == "" || descr == "" || initialDate == "" || idx == "")
                return BadRequest();
            var list = lists.GetById(lid);

            var year = initialDate.Substring(0, 4);
            var day = initialDate.Substring(8);
            var array = initialDate.ToCharArray();
            var month = (array[5] - '0')*10 + (array[6] - '0');
            var initialD = new DateTime(Convert.ToInt32(year), month, Convert.ToInt32(day));
            //Incrementar a ordem dos cartões que tenham idx igual a superior ao idx do cartão inserido
            cards.SetList(cards.GetAllByList(lists.GetById(lid)).OrderBy(card => card.idx >= Convert.ToInt32(idx) ? card.idx++ : card.idx).ToList());

            if(dueDate == "")
                cards.Add(new Card(list, nextCID++, name, descr, initialD, Convert.ToInt32(idx)));
            else
            {
                year = dueDate.Substring(0, 4);
                array = dueDate.ToCharArray();
                month = (array[5] - '0') * 10 + (array[6] - '0');
                day = dueDate.Substring(8);
                var dueD = new DateTime(Convert.ToInt32(year), month, Convert.ToInt32(day));
                cards.Add(new Card(list, nextCID++, name, descr, initialD, dueD, Convert.ToInt32(idx)));
            }
            //Ordenar a lista
            cards.SetList(cards.GetAllByList(lists.GetById(lid)).OrderBy(card => card.idx).ToList());
            var resp = new HttpResponseMessage(HttpStatusCode.SeeOther);
            resp.Headers.Location = new Uri(ResolveUri.ForList(list));
            return resp;
        }


        [HttpMethod("POST", "/boards/{bid}/lists/{lid}/cards/{cid}")]
        public HttpResponseMessage Post(NameValueCollection content, int bid, int lid, int cid)
        {
            var c = cards.GetById(cid);
            var list = lists.GetById(lid);
            var newIdx = content["kix"];
            var newId = content["lid"];
            int idx;
            if(newIdx == "" && newId == "")
                return BadRequest();

            if (newId != "")
            {
                lid = Convert.ToInt32(newId);
                list = lists.GetById(lid);
                c.list = list;
            }
            if (newIdx != "")
                idx = Convert.ToInt32(newIdx);
            else
                idx = c.idx;
            if (idx < c.idx)
                foreach (var card in cards.GetAllByList(list))
                {
                    if (card.list.id == lid && card.idx <= idx)
                    {
                        ++card.idx;
                    }
                }
            else
                foreach (var card in cards.GetAllByList(list))
                {
                    if (card.list.id == lid && card.idx >= idx)
                    {
                        --card.idx;
                    }
                }
            c.idx = idx;
            var resp = new HttpResponseMessage(HttpStatusCode.SeeOther);
            resp.Headers.Location = new Uri(ResolveUri.ForList(list));
            return resp;
        }

        [HttpMethod("POST", "/boards/{bid}/lists/{lid}/toremove")]
        public HttpResponseMessage PostRemoveList(NameValueCollection content, int bid, int lid)
        {
            if (lists.GetById(lid) == null || boards.GetById(bid) == null)
                return BadRequest();
            if (cards.GetAllByList(lists.GetById(lid)).Count() == 0)
                lists.Remove(lists.GetById(lid));
            var resp = new HttpResponseMessage(HttpStatusCode.SeeOther);
            resp.Headers.Location = new Uri(ResolveUri.ForBoard(boards.GetById(bid)));
            return resp;
        }

        public HttpResponseMessage NotFound()
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("Resource Not Found")
            };
        }

        public HttpResponseMessage BadRequest()
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Client Error: Bad Request")
            };
        }
    }
}
