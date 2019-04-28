using Sagromin.ViewModels.Product;
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
    public class ProductController : BaseController
    {
        public ActionResult ListProduct(Int32? p)
        {
            var vm = new ListProductViewModel();
            vm.Fill(CargarDatosContext(), p);
            return View(vm);
        }

        public ActionResult _AddEditProduct(Int32? ProductId)
        {
            var vm = new _AddEditProductViewModel();
            vm.Fill(CargarDatosContext(), ProductId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult _AddEditProduct(_AddEditProductViewModel model)
        {
            try
            {
                var existe = context.Product.FirstOrDefault(x => x.Name.Contains(model.Name) && x.Status == ConstantHelpers.ESTADO.ACTIVO);
                using (var ts = new TransactionScope())
                {

                    Product product = new Product();
                    if (model.ProductId.HasValue)
                    {
                        product = context.Product.FirstOrDefault(x => x.ProductId == model.ProductId);
                    }
                    else
                    {
                        if (existe != null)
                        {
                            PostMessage(MessageType.Info, "producto ya registrado");
                            return RedirectToAction("ListProduct");
                        }

                        context.Product.Add(product);
                        product.Status = ConstantHelpers.ESTADO.ACTIVO;
                        product.Creation_Date = DateTime.Now;
                    }

                    product.Name = model.Name;
                    
                    context.SaveChanges();
                    ts.Complete();
                }
                PostMessage(MessageType.Success, "Producto Guardado");
                return RedirectToAction("ListProduct");
            }
            catch (Exception e)
            {
                return View(model);
            }

        }

        [HttpPost]
        public JsonResult _DeleteProduct(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var product = context.Product.FirstOrDefault(x => x.ProductId == Id);
                        product.Status = ConstantHelpers.ESTADO.INACTIVO;
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