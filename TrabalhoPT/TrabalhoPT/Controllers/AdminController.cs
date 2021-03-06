﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoPT.DataMappers;
using TrabalhoPT.Models;

namespace TrabalhoPT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [HttpGet]
        public ActionResult ControlPanel()
        {
            return View();
        }

        //
        // GET: /Admin/
        [HttpGet]
        public ActionResult ToUser(string id)
        {
            var acc = AccountDataMapper.GetAccountDataMapper().GetById(id);
            if (acc.Roles.Contains("Admin"))
                acc.Roles.Remove("Admin");
            return RedirectToAction("ControlPanel");
        }

        //
        // GET: /Admin/
        [HttpGet]
        public ActionResult ToAdmin(string id)
        {
            var acc = AccountDataMapper.GetAccountDataMapper().GetById(id);
            if (!acc.Roles.Contains("Admin"))
                acc.Roles.Add("Admin");
            return RedirectToAction("ControlPanel");
        }

    }
}
