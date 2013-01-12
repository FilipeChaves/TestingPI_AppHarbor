using System;
using System.Linq;
using System.Web.Mvc;
using TrabalhoPT.DataMappers;
using TrabalhoPT.Models;

namespace TrabalhoPT.Controllers
{
    public class BoardsController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var bm = BoardDataMapper.GetBoardDataMapper();
            var boards =
                bm.GetBoardsFrom(AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name));
            return View(boards);
        }

        //
        // GET: /Boards/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View(new BoardsModel());
        }

        //
        // GET: /Boards/Create
        [HttpGet]
        [Authorize]
        public ActionResult GetLists(int id)
        {
            var board = BoardDataMapper.GetBoardDataMapper().GetById(id);
            if (board == null)
                return Redirect("~/Errors/Http404");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanReadBoard(board.Id))
                return RedirectToAction("Index");
            var ie = ListDataMapper.GetListDataMapper().GetAllByBoard(board);
            if (ie.Count() != 0)
                return View(ie);
            return View("Empty", board);
        }

        //
        // POST: /Boards/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(BoardsModel boardsModel)
        {
            var bm = BoardDataMapper.GetBoardDataMapper();
            if (bm.GetByName(boardsModel.Name) == null){
                bm.Add(boardsModel);
                var acc = AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name);
                acc.AddBoard(boardsModel);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Name", "Já existe um Quadro com esse nome");
            return View(boardsModel);
        }

        //
        // GET: /Boards/CreateList/{id}
        [HttpGet]
        [Authorize]
        public ActionResult CreateList(int id)
        {
            var board = BoardDataMapper.GetBoardDataMapper().GetById(id);
            if(board == null)
                return Redirect("~/Errors/Http404");
            return View(new ListsModel{ Board = board });
        }

        //
        // POST: /Boards/CreateList/{id}
        [HttpPost]
        [Authorize]
        public ActionResult CreateList(ListsModel listsModel, int id)
        {
            var board = BoardDataMapper.GetBoardDataMapper().GetById(id);
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(board.Id))
                return RedirectToAction("Index");
            listsModel.Board = board;
            var ldm = ListDataMapper.GetListDataMapper();
            ldm.Add(listsModel);
            return Redirect(String.Format("~/Boards/GetLists/{0}", id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var board = BoardDataMapper.GetBoardDataMapper().GetById(id);
            if (board == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(board.Id))
                return RedirectToAction("Index");
            return View(board);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(BoardsModel boardsModel)
        {
            var board = BoardDataMapper.GetBoardDataMapper().GetById(boardsModel.Id);
            if (board == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(board.Id))
                return RedirectToAction("Index");
            board.Name = boardsModel.Name;
            board.Description = boardsModel.Description;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GiveReadRights(int id)
        {
            var board = BoardDataMapper.GetBoardDataMapper().GetById(id);
            if (board == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(board.Id))
                return RedirectToAction("Index");
            return View(new GiveRightsModel { BoardId = id} );
        }

        [HttpPost]
        [Authorize]
        public ActionResult GiveReadRights(GiveRightsModel gr)
        {
            var acc = AccountDataMapper.GetAccountDataMapper().GetById(gr.Name);
            if (acc == null)
            {
                ModelState.AddModelError("Name", "Não existe nenhum utilizador com esse username");
                return View(gr);
            }
            var board = BoardDataMapper.GetBoardDataMapper().GetById(gr.BoardId);
            if (board == null)
            //return RedirectToAction("Http404", "Errors");
            {
                ModelState.AddModelError("Name", "BoardId " + gr.BoardId);
                return View(gr);
            }
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(board.Id))
                return RedirectToAction("Index");
            acc.AddReadBoard(board.Id);
            return RedirectToAction("GetLists", new {id = board.Id});
        }

        [HttpGet]
        [Authorize]
        public ActionResult GiveWriteRights(int id)
        {
            var board = BoardDataMapper.GetBoardDataMapper().GetById(id);
            if (board == null)
                return RedirectToAction("Http404", "Errors");
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(board.Id))
                return RedirectToAction("Index", "Boards");
            return View(new GiveRightsModel { BoardId = id });
        }

        [HttpPost]
        [Authorize]
        public ActionResult GiveWriteRights(GiveRightsModel gr)
        {
            var acc = AccountDataMapper.GetAccountDataMapper().GetById(gr.Name);
            if (acc == null)
            {
                ModelState.AddModelError("Name", "Não existe nenhum utilizador com esse username");
                return View(gr);
            }
            var board = BoardDataMapper.GetBoardDataMapper().GetById(gr.BoardId);
            if (board == null)
                //return RedirectToAction("Http404", "Errors");
            {
                ModelState.AddModelError("Name", "BoardId " + gr.BoardId);
                return View(gr);
            }
            if (!AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name).CanWriteBoard(board.Id))
                return RedirectToAction("Index");
            acc.AddWriteBoard(board.Id);
            return RedirectToAction("GetLists", new { id = board.Id });
        }

        [HttpGet]
        [Authorize]
        public bool BoardExists(String id)
        {
            var user = AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name);
            var board = BoardDataMapper.GetBoardDataMapper().GetBoardByUserAndName(user, id);
            return board != null;
        }

        [HttpGet]
        [Authorize]
        public JsonResult ExistingLists(int id)
        {
            var user = AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name);
            return user.CanReadBoard(id) ? Json(
                ListDataMapper.GetListDataMapper().GetAllByBoard(BoardDataMapper.GetBoardDataMapper().GetById(id)), JsonRequestBehavior.AllowGet) : null;
        }
    }
}