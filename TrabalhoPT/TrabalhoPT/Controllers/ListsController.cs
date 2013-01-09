using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoPT.DataMappers;
using TrabalhoPT.Models;

namespace TrabalhoPT.Controllers
{
    public class ListsController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult GetCards(int id){
            var list = ListDataMapper.GetListDataMapper().GetById(id);
            if (list == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanReadBoard(list.Board.Id))
                return RedirectToAction("Index", "Boards");
            var cm = CardDataMapper.GetCardDataMapper().GetAllByList(list);
            if(cm.Count() != 0)
                return View(cm);
            return View("Empty", list);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateCard(int id){
            var list = ListDataMapper.GetListDataMapper().GetById(id);
            if (list == null)
                return Redirect("~/Errors/Http404");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(list.Board.Id))
                return RedirectToAction("Index", "Boards");
            return View(new CardsModel{ List = list });
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateCard(CardsModel c, int id){
            c.List = ListDataMapper.GetListDataMapper().GetById(id);
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(c.List.Board.Id))
                return RedirectToAction("Index", "Boards");
            CardDataMapper.GetCardDataMapper().Add(c);
            return Redirect(String.Format("~/Lists/GetCards/{0}", id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var list = ListDataMapper.GetListDataMapper().GetById(id);
            if (list == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(list.Board.Id))
                return RedirectToAction("Index", "Boards");
            return View(list);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(ListsModel listsModel)
        {
            var list = ListDataMapper.GetListDataMapper().GetById(listsModel.Id);
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(list.Board.Id))
                return RedirectToAction("Index", "Boards");
            list.Name = listsModel.Name;
            return RedirectToAction("GetLists", "Boards", new {id = list.Board.Id});
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemoveList(int id)
        {
            
            var list = ListDataMapper.GetListDataMapper().GetById(id);
            if (list == null)
                return RedirectToAction("Http404", "Errors");
            if (CardDataMapper.GetCardDataMapper().GetAllByList(list).Count() != 0)
                return RedirectToAction("GetCards", new { id });
			if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(list.Board.Id))
                return RedirectToAction("Index", "Boards");
            return View(list);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RemoveList(ListsModel listsModel)
        {
            var list = ListDataMapper.GetListDataMapper().GetById(listsModel.Id);
            int boardId = list.Board.Id;
			if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(boardId))
                return RedirectToAction("Index", "Boards");
            ListDataMapper.GetListDataMapper().Remove(list);
            return RedirectToAction("GetLists", "Boards", new { id = boardId });
        }
    }
}