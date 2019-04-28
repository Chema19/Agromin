using Sagromin.ViewModels.ProductBrand;
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
    public class ProductBrandController : BaseController
    {
        public ActionResult ListProductBrand(Int32? p)
        {
            var vm = new ListProductBrandViewModel();
            vm.Fill(CargarDatosContext(), p);
            return View(vm);
        }

        public ActionResult _AddEditProductBrand(Int32? ProductBrandId)
        {
            var vm = new _AddEditProductBrandViewModel();
            vm.Fill(CargarDatosContext(), ProductBrandId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult _AddEditProductBrand(_AddEditProductBrandViewModel model)
        {
            try
            {
                var existe = context.ProductBrand.FirstOrDefault(x => x.ProductId == model.ProductId && x.BrandId == model.BrandId && x.Status == ConstantHelpers.ESTADO.ACTIVO);

                using (var ts = new TransactionScope())
                {

                    ProductBrand ProductBrand = new ProductBrand();
                    if (model.ProductBrandId.HasValue)
                    {
                        ProductBrand = context.ProductBrand.FirstOrDefault(x => x.ProductBrandId == model.ProductBrandId);
                    }
                    else
                    {
                        if (existe != null)
                        {
                            PostMessage(MessageType.Info, "Marca producto ya registrado");
                            return RedirectToAction("ListProductBrand");
                        }

                        context.ProductBrand.Add(ProductBrand);
                        ProductBrand.Status = ConstantHelpers.ESTADO.ACTIVO;
                    }
                    
                    ProductBrand.BrandId = model.BrandId;
                    ProductBrand.ProductId = model.ProductId;

                    context.SaveChanges();

                    //CREAR STOCK PRODUCTO
                   
                    if (!model.ProductBrandId.HasValue)
                    {
                        StockProduct stockProduct = new StockProduct();
                        context.StockProduct.Add(stockProduct);
                        stockProduct.Amount = 0;
                        stockProduct.ProductBrandId = ProductBrand.ProductBrandId;
                        stockProduct.Status = ConstantHelpers.ESTADO.ACTIVO;
                        context.SaveChanges();
                    }
                    
                    ts.Complete();
                }
                PostMessage(MessageType.Success, "Marca Producto Guardado");
                return RedirectToAction("ListProductBrand");
            }
            catch (Exception e)
            {
                return View(model);
            }

        }

        [HttpPost]
        public JsonResult _DeleteProductBrand(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var StockProduct = context.StockProduct.FirstOrDefault(x => x.ProductBrandId == Id);

                        if (StockProduct.Amount == 0)
                        {
                            var ProductBrand = context.ProductBrand.FirstOrDefault(x => x.ProductBrandId == Id);
                            ProductBrand.Status = ConstantHelpers.ESTADO.INACTIVO;
                            //INACTIVO STOCK
                            StockProduct.Status = ConstantHelpers.ESTADO.INACTIVO;
                            context.SaveChanges();
                            validacion = true;
                        }
                        else {
                            validacion = false;
                        }                    
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