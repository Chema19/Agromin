using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sagromin.ViewModels.User;
using System.Transactions;
using Sagromin.Models;
using Sagromin.Helpers;
using Sagromin.Logics;

namespace Sagromin.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult ListUser(Int32? p)
        {
            var vm = new ListUserViewModel();
            vm.Fill(CargarDatosContext(), p);
            return View(vm);
        }

        public ActionResult _AddEditUser(Int32? UserId)
        {
            var vm = new _AddEditUserViewModel();
            vm.Fill(CargarDatosContext(), UserId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult _AddEditUser(_AddEditUserViewModel model)
        {
            try {
                var dniExiste = context.User.FirstOrDefault(x => x.Identity_Document == model.Identity_Document);

                if (model.Identity_Document.Length != 8)
                {
                    PostMessage(MessageType.Error, "El DNI debe de contener 8 caracteres");
                    return RedirectToAction("ListUser");
                }

                using (var ts = new TransactionScope()) {

                    User user = new User();
                    if (model.UserId.HasValue) {
                        user = context.User.FirstOrDefault(x => x.UserId == model.UserId);
                        user.Update_date = DateTime.Now;
                    }
                    else {
                        if (dniExiste != null)
                        {
                            PostMessage(MessageType.Info, "Usuario ya registrado");
                            return RedirectToAction("ListUser");
                        }

                        context.User.Add(user);
                        user.Status = ConstantHelpers.ESTADO.ACTIVO;
                        user.Creation_date = DateTime.Now;
                        user.Update_date = DateTime.Now;
                    }

                    user.Names = model.Names;
                    user.Last_Names = model.LastNames;
                    user.Credential = model.Credential;
                    user.Password = model.Password;
                    user.Sex = model.Sex;
                    user.Identity_Document = model.Identity_Document;
                    user.Email = model.Email;
                    user.Birthdate = model.Birthdate;
                    user.LocalId = model.LocalId;
                    user.DistrictId = model.DistrictId;
                    user.Master = "";
                    user.Phone = model.Phone;

                    context.SaveChanges();
                    ts.Complete();
                }
                PostMessage(MessageType.Success, "Usuario Guardado");
                return RedirectToAction("ListUser");
            } catch (Exception e) {
                return View(model);
            }
            
        }

        [HttpPost]
        public JsonResult _DeleteUser(Int32? Id) {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var user = context.User.FirstOrDefault(x => x.UserId == Id);
                        user.Status = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                        validacion = true;
                    }
                    ts.Complete();
                }
                return Json(new { validacion });
            }
            catch (Exception e) {
                return Json(new { });
            }
        }
    }
}