using Sagromin.Helpers;
using Sagromin.Models;
using Sagromin.ViewModels.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Sagromin.Controllers
{
    public class LocalController : BaseController
    {
        // GET: Local
        public ActionResult ListLocal(Int32? p)
        {
            var vm = new ListLocalViewModel();
            vm.Fill(CargarDatosContext(), p);
            return View(vm);
        }

        public ActionResult _AddEditLocal(Int32? LocalId)
        {
            var vm = new _AddEditLocalViewModel();
            vm.Fill(CargarDatosContext(), LocalId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult _AddEditLocal(_AddEditLocalViewModel model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Local local = new Local();
                    if (model.LocalId.HasValue)
                    {
                        local = context.Local.FirstOrDefault(x => x.LocalId == model.LocalId);
                    }
                    else
                    {
                        context.Local.Add(local);
                        local.Status = ConstantHelpers.ESTADO.ACTIVO;
                    }

                    local.Name = model.Name;
                    local.DistrictId = model.DistrictId;
                    local.Phone = model.Phone;
                    local.Address = model.Address;

                    context.SaveChanges();
                    ts.Complete();
                }
                PostMessage(MessageType.Success, "local Guardado");
                return RedirectToAction("ListLocal");
            }
            catch (Exception e)
            {
                return View(model);
            }

        }

        [HttpPost]
        public JsonResult _DeleteLocal(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var user = context.Local.FirstOrDefault(x => x.LocalId == Id);
                        user.Status = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                        validacion = true;
                    }
                    ts.Complete();
                }
                return Json(new { validacion });
            }
            catch (Exception e)
            {
                return Json(new { });
            }
        }
    }
}