using Sagromin.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using Sagromin.Helpers;

namespace Sagromin.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Login()
        {
            var vm = new LoginViewModel();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try {
                using (var ts = new TransactionScope())
                {
                    if (String.IsNullOrEmpty(model.Credential) && String.IsNullOrEmpty(model.Password))
                    {
                        PostMessage(MessageType.Info, "Ingrese usuario y contraseña");
                        return View(model);
                    }
                    else if (String.IsNullOrEmpty(model.Credential) || String.IsNullOrEmpty(model.Password))
                    {
                        if (String.IsNullOrEmpty(model.Credential))
                        {
                            PostMessage(MessageType.Info, "Ingrese su usuario correctamente");
                            return View(model);
                        }
                        else
                        {
                            PostMessage(MessageType.Info, "Ingrese su contraseña correctamente");
                            return View(model);
                        }
                    }
                    else if(!String.IsNullOrEmpty(model.Credential) && !String.IsNullOrEmpty(model.Password))
                    {
                        var user = context.User.FirstOrDefault(x => x.Credential == model.Credential && x.Password == model.Password);
                        if (user != null)
                        {
                            Session.Set(SessionKey.UserId, user.UserId);
                            Session.Set(SessionKey.FullName, user.Names + " " + user.Last_Names);
                            Session.Set(SessionKey.LocalId, user.LocalId);

                            return RedirectToAction("ListSale", "Sale");
                        }
                        else {
                            PostMessage(MessageType.Info, "El usuario ingresado no existe");
                            return View(model);
                        }
                    }
                    ts.Complete();
                }
                return View(model);
            } catch (Exception e){
                var vm = new LoginViewModel();
                return View(vm);
            }
        }
    }
}