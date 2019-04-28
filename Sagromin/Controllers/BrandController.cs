using Sagromin.ViewModels.Brand;
using Sagromin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Sagromin.Helpers;
namespace Sagromin.Controllers
{
    public class BrandController : BaseController
    {
        public ActionResult ListBrand(Int32? p)
        {
            var vm = new ListBrandViewModel();
            vm.Fill(CargarDatosContext(), p);
            return View(vm);
        }

        public ActionResult _AddEditBrand(Int32? BrandId)
        {
            var vm = new _AddEditBrandViewModel();
            vm.Fill(CargarDatosContext(), BrandId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult _AddEditBrand(_AddEditBrandViewModel model)
        {
            try
            {
                var existe = context.Brand.FirstOrDefault(x => x.Name.Contains(model.Name) && x.Status == ConstantHelpers.ESTADO.ACTIVO);
                using (var ts = new TransactionScope())
                {

                    Brand Brand = new Brand();
                    if (model.BrandId.HasValue)
                    {
                        Brand = context.Brand.FirstOrDefault(x => x.BrandId == model.BrandId);
                    }
                    else
                    {
                        if (existe != null)
                        {
                            PostMessage(MessageType.Info, "Marca ya registrada");
                            return RedirectToAction("ListBrand");
                        }

                        context.Brand.Add(Brand);
                        Brand.Status = ConstantHelpers.ESTADO.ACTIVO;
                    }

                    Brand.Name = model.Name;

                    context.SaveChanges();
                    ts.Complete();
                }
                PostMessage(MessageType.Success, "Brando Guardado");
                return RedirectToAction("ListBrand");
            }
            catch (Exception e)
            {
                return View(model);
            }

        }

        [HttpPost]
        public JsonResult _DeleteBrand(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var Brand = context.Brand.FirstOrDefault(x => x.BrandId == Id);
                        Brand.Status = ConstantHelpers.ESTADO.INACTIVO;
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