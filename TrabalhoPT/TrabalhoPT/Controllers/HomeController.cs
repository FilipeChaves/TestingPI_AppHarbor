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
            //var result = new List<String>();
            BoardDataMapper b = BoardDataMapper.GetBoardDataMapper();
            foreach (BoardsModel bm in b.GetAll())
            {
                if (counter == 0)
                    goto return_label;
                if (bm.Name.Contains(id))
                {
                    result.Add(new KeyValuePair<String, String>(bm.Name, "/Boards/GetLists/" + bm.Id));
                    //result.Add(bm.Name);
                    --counter;
                }
            }
            ListDataMapper l = ListDataMapper.GetListDataMapper();
            foreach (ListsModel bm in l.GetAll())
            {
                if (counter == 0)
                    goto return_label;
                if (bm.Name.Contains(id))
                {
                    result.Add(new KeyValuePair<String, String>(bm.Name, "/Lists/GetCards/" + bm.Id));
                    //result.Add(bm.Name);
                    --counter;
                }
            }
            CardDataMapper c = CardDataMapper.GetCardDataMapper();
            foreach (CardsModel bm in c.GetAll())
            {
                if (counter == 0)
                    goto return_label;
                if (bm.Name.Contains(id))
                {
                    result.Add(new KeyValuePair<String, String>(bm.Name, "/Cards/GetCard/" + bm.Id));
                    //result.Add(bm.Name);
                    --counter;
                }
            }
            
            return_label: return PartialView("Search",result);
        }

    }
}
