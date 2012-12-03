using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Etapa1.DataMappers;
using Etapa1.Models;

namespace Etapa1.Controllers
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

    }
}
