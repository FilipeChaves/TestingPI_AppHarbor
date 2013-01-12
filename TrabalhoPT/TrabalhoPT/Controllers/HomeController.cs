using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoPT.DataMappers;
using TrabalhoPT.Models;

namespace TrabalhoPT.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HttpGet]
        [Authorize]
        public ActionResult Index(AccountModel accountModel)
        {
            return View(accountModel);
        }

        //
        // GET: /Home/
        public ActionResult Search(String id)
        {
            int counter = 5;
            var result = new List<KeyValuePair<String, String>>();

            BoardDataMapper b = BoardDataMapper.GetBoardDataMapper();
            
            //Guarda os boards do user e procura boards cujo nome contenha id
            IEnumerable<BoardsModel> boards = b.GetBoardsFrom(AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name));
            GetResultsByName(boards, "/Boards/GetLists/", result, counter, id);
            
            //No caso de ainda poderem ser adicionados resultados adiciona lists
            if (counter > 0)
            {
                //Procura listas
                var allLists = new List<ListsModel>();
                ListDataMapper l = ListDataMapper.GetListDataMapper();
                foreach(BoardsModel bm in boards)
                {
                    IEnumerable<ListsModel> lists = l.GetAllByBoard(bm);
                    allLists.AddRange(lists);
                    GetResultsByName(lists, "/Lists/GetCards/", result, counter, id);
                }

                //Se ainda poderem ser adicionados resultados adiciona cards
                if(counter>0)
                {
                    var allCards = new List<CardsModel>();
                    CardDataMapper c = CardDataMapper.GetCardDataMapper();
                    foreach (ListsModel lm in allLists)
                    {
                        IEnumerable<CardsModel> cards = c.GetAllByList(lm);
                        allCards.AddRange(cards);
                        GetResultsByName(cards, "/Cards/GetCard/",result,counter,id);
                    }

                    //Se ainda poderem ser adicionados resultados adiciona cards pela descrição
                    if (counter > 0)
                    {
                        GetResultsByDescription(allCards,"/Cards/GetCard/", result, counter, id);
                    }
                }
            }
            return PartialView("Search",result);
        }

        public void GetResultsByName(IEnumerable<BLCModel> datamapper, String url, List<KeyValuePair<String, String>> result, int counter, String id)
        {
            foreach (BLCModel bm in datamapper)
            {
                if (counter == 0)
                    return;
                if (bm.Name.ToLower().Contains(id.ToLower()))
                {
                    result.Add(new KeyValuePair<String, String>(bm.Name, url + bm.Id));
                    --counter;
                }
            }
        }

        //Mesmo que o cartão ja tenha sido adicionado, caso a descrição tenha ID volta a adicionar
        public void GetResultsByDescription(IEnumerable<CardsModel> datamapper, String url, List<KeyValuePair<String, String>> result, int counter, String id)
        {
            foreach (CardsModel cm in datamapper)
            {
                if (counter == 0)
                    return;
                if (cm.Description != null && cm.Description.ToLower().Contains(id.ToLower()))
                {
                    result.Add(new KeyValuePair<String, String>(cm.Name, url + cm.Id));
                    --counter;
                }
            }
        }
    }
}
