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
        [HttpGet]
        public String Search(String id)
        {
            return "cenas";
            /*
            BoardDataMapper b = BoardDataMapper.GetBoardDataMapper();
            b.GetBoardsFrom(User.Identity);



            
            return Json(new
                            {
                                Hour = now.Hour,
                                Minute = now.Minute,
                                Second = now.Second
                            }, JsonRequestBehavior.AllowGet );*/

        }

    }
}
