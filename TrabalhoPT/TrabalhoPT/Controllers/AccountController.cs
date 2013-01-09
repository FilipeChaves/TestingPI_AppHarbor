using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Net.NetworkInformation;
using TrabalhoPT.DataMappers;
using TrabalhoPT.Models;
using TrabalhoPT.Utils;

namespace TrabalhoPT.Controllers
{
    /*
     * Bibliography: http://msdn.microsoft.com/en-us/library/aa302398.aspx
     * 
     */

    public class AccountController : Controller
    {
        //
        // GET: /Account/LogOn
        [HttpGet]
        public ActionResult LogOn()
        {
            FormsAuthentication.SignOut();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home",
                                        AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name));
            }
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home",
                                        AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name));
            return View();
        }

        // Get: /Account/Confirmation
        [HttpGet]
        public ActionResult Confirmation()
        {
            return View();
        }

        // Get: /Account/Verify
        [HttpGet]
        public ActionResult Verify(String id)
        {
            var toConfirm = ToConfirmDataMapper.GetAccountDataMapper();
            var user = toConfirm.GetById(id);
            if (user == null)
                return View();
            user.Confirmed = true;
            toConfirm.Remove(user);
            return RedirectToAction("LogOn", "Account");
        }

        [HttpGet]
        public ActionResult Settings(AccountModel accountModel)
        {
            return View(accountModel);
        }

        [HttpGet]
        [Authorize]
        public ViewResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangeEmail()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult SelfDelete()
        {
            FormsAuthentication.SignOut();
            return Delete(User.Identity.Name);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (User.IsInRole("Admin") || User.Identity.Name.Equals(id))
            {
                var accs = AccountDataMapper.GetAccountDataMapper();
                accs.Remove(accs.GetById(id));
                return RedirectToAction("ControlPanel", "Admin"); //ToDo
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult SetImage()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ShowInfo(AccountModel accountModel)
        {
            return View(accountModel);
        }

       	
		// POST: /Account/LogOn
        [HttpPost]
        public ActionResult LogOn(AccountModel accountModel){
            var adm = AccountDataMapper.GetAccountDataMapper();
            var user = adm.GetById(accountModel.Username.ToLower());
            if (user != null){
				if (!user.Confirmed)
                    ModelState.AddModelError("Username", "O username inserido ainda não foi confirmado. Por favor confirme através do seu email.");
                else if (LoginUtils.ComparePasswords(accountModel.Password, user))
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home", user);
                }
                /* Login Failed */
                else
					ModelState.AddModelError("Password", "A password inserida não é valida");
            }
            else/*User not found. Please register*/
				ModelState.AddModelError("Username", "O username inserido não corresponde a nenhum utilizador registado.");
            return View(accountModel);
        }

        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(RegisterAccountModel accountModel)
        {
            AccountModel acc = LoginUtils.RegisterAccount(accountModel);
            if (acc != null)
            {
                LoginUtils.SendConfirmationMail(acc);
                return RedirectToAction("Confirmation", "Account");
            }
            return Register();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = User.Identity.Name;
            var adm = AccountDataMapper.GetAccountDataMapper();
            var acc = adm.GetById(user);
            if (ModelState.IsValid)
            {
                if (!LoginUtils.ComparePasswords(changePasswordModel.OldPw, acc))
                {
                    ModelState.AddModelError("OldPw", "Password incorrecta!");
                    return View(changePasswordModel);
                }
                acc.Password = changePasswordModel.Pw1;
                LoginUtils.EncryptPassword(acc);
                return View("PasswordChanged");
            }
            return View(changePasswordModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeEmail(ChangeEmailModel changeEmailModel)
        {
            var user = User.Identity.Name;
            var adm = AccountDataMapper.GetAccountDataMapper();
            var acc = adm.GetById(user);
            if (ModelState.IsValid)
            {
                if (!changeEmailModel.OldEmail.Equals(acc.Email))
                {
                    ModelState.AddModelError("OldEmail", "Esta conta não está associada a este email");
                    return View(changeEmailModel);
                }
                acc.Email = changeEmailModel.NewEmail;
                LoginUtils.AddToConfirmList(acc);
                FormsAuthentication.SignOut();
                return View("Confirmation");
            }
            return View(changeEmailModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SetImage(FormCollection fc)
        {
            var user = AccountDataMapper.GetAccountDataMapper().GetById(User.Identity.Name);
            user.ImageUrl = fc["ImageUrl"];
            return RedirectToAction("ShowInfo", user);
        }
    }
}
