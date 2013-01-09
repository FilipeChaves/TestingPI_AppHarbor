using TrabalhoPT.DataMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoPT.Models;

namespace TrabalhoPT.Controllers
{
    public class CardsController : Controller
    {
        // GET: /Cards/GetCard/{id}
        [HttpGet]
        [Authorize]
        public ActionResult GetCard(int id)
        {
            var card = CardDataMapper.GetCardDataMapper().GetById(id);
            if (card == null)
                return Redirect("~/Errors/Http404");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanReadBoard(card.List.Board.Id))
                return RedirectToAction("Index", "Boards");
            return View(card);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var card = CardDataMapper.GetCardDataMapper().GetById(id);
            if (card == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(card.List.Board.Id))
                return RedirectToAction("Index", "Boards");
            return View(card);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(CardsModel cardsModel)
        {
            var card = CardDataMapper.GetCardDataMapper().GetById(cardsModel.Id);
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(card.List.Board.Id))
                return RedirectToAction("Index", "Boards");
            card.Name = cardsModel.Name;
            if(card.List != null)
                return RedirectToAction("GetCards", "Lists", new {id = card.List.Id});
            return RedirectToAction("GetCard", new { card.Id });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Archive(int id)
        {
            var card = CardDataMapper.GetCardDataMapper().GetById(id);
            if (card == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(card.List.Board.Id))
                return RedirectToAction("Index", "Boards");
            if (card.List == null)
                return RedirectToAction("GetCard", new { id });
            return View(card);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Archive(CardsModel cardsModel)
        {
            var card = CardDataMapper.GetCardDataMapper().GetById(cardsModel.Id);
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(card.List.Board.Id))
                return RedirectToAction("Index", "Boards");
            var listId = card.List.Id;
            int cardIdx = card.Idx;
            RefreshIdx(CardDataMapper.GetCardDataMapper().GetAllByList(card.List), cardIdx);
            card.List = null;
            return RedirectToAction("GetCards", "Lists", new { id = listId } );
        }

        [HttpGet]
        [Authorize]
        public ActionResult MoveCardInList(int id)
        {
            var card = CardDataMapper.GetCardDataMapper().GetById(id);
            if (card == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(card.List.Board.Id))
                return RedirectToAction("Index", "Boards");
            return View(card);
        }

        [HttpPost]
        [Authorize]
        public ActionResult MoveCardInList(CardsModel cardsModel)
        {
            var card = CardDataMapper.GetCardDataMapper().GetById(cardsModel.Id);
            int oldIdx = card.Idx;
            int newIdx = cardsModel.Idx;
            IEnumerable<CardsModel> cardsEnum = CardDataMapper.GetCardDataMapper().GetAllByList(card.List);
            int nCards = cardsEnum.Count();

            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(card.List.Board.Id))
                return RedirectToAction("Index", "Boards");

            if (nCards < newIdx)
                newIdx = nCards;

            if (oldIdx < newIdx) {
                foreach (var c in cardsEnum) {
                    if (c.Idx > oldIdx && c.Idx <= newIdx)
                        --c.Idx;
                }
            }
            else if (newIdx < oldIdx) {
                foreach (var c in cardsEnum) {
                    if (c.Idx < oldIdx && c.Idx >= newIdx)
                        ++c.Idx;
                }
            }
            card.Idx = newIdx;

            return RedirectToAction("GetCards", "Lists", new {id = card.List.Id});
        }
        [HttpGet]
        [Authorize]
        public ActionResult MoveCardToList(int id)
        {
            return View(new MoveToListModel { CardId = id });
        }

        [HttpPost]
        [Authorize]
        public ActionResult MoveCardToList(MoveToListModel move)
        {
            var newList = ListDataMapper.GetListDataMapper().GetById(move.ListId);
            if (newList == null)
            {
                ModelState.AddModelError("ListId", "Essa lista nao existe!");
                return View(move);
            }
            var cm = CardDataMapper.GetCardDataMapper();
            var card = cm.GetById(move.CardId);
            var cardsList = cm.GetAllByList(newList);
            if(card.List != null)
            {
                RefreshIdx(cm.GetAllByList(newList), card.Idx);
            }
            card.List = newList;
            card.Idx = cardsList.Count();
            return RedirectToAction("GetCards", "Lists", new {id = move.ListId});
        }

        private static void RefreshIdx(IEnumerable<CardsModel> cardsList, int oldIdx)
        {
            foreach (var card in cardsList)
            {
                if(card.Idx > oldIdx)
                {
                    --card.Idx;
                }
            }
        }
    }
}