using Sagromin.Helpers;
using Sagromin.Models;
using Sagromin.ViewModels.StockProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Sagromin.Controllers
{
    public class StockProductController : BaseController
    {
        // GET: StockProduct
        public ActionResult ListStockProduct(Int32? p)
        {
            var vm = new ListStockProductViewModel();
            vm.Fill(CargarDatosContext(), p);
            return View(vm);
        }
        public ActionResult ListEntryExitProduct(Int32? p, Int32? ProductBrandId)
        {
            var vm = new ListEntryExitProductViewModel();
            vm.Fill(CargarDatosContext(), p, ProductBrandId);
            return View(vm);
        }
        public ActionResult _AddEditEntryExitProduct()
        {
            var vm = new _AddEditEntryExitProductViewModel();
            vm.Fill(CargarDatosContext());
            return View(vm);
        }
        [HttpPost]
        public ActionResult _AddEditEntryExitProduct(_AddEditEntryExitProductViewModel model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    EntryExitProduct entryExitProduct = new EntryExitProduct();
                    if (model.EntryExitProductId.HasValue)
                    {
                        entryExitProduct = context.EntryExitProduct.FirstOrDefault(x => x.EntryExitProductId == model.EntryExitProductId);
                    }
                    else
                    {
                        context.EntryExitProduct.Add(entryExitProduct);
                        entryExitProduct.StatusType = ConstantHelpers.ESTADO.ENTRADA;
                        entryExitProduct.Creation_Date = DateTime.Now;
                    }

                    entryExitProduct.Amount = model.Amount;
                    entryExitProduct.UserId = Session.GetUserId();
                    entryExitProduct.ProductBrandId = model.ProductBrandId;
                    context.SaveChanges();
                    //INSERTAR EN EL STOCK

                    Decimal? entradasProducto = context.EntryExitProduct.Where(x => x.StatusType == ConstantHelpers.ESTADO.ENTRADA && x.ProductBrandId == model.ProductBrandId).Sum(x => x.Amount);
                    Decimal? salidasProducto = context.EntryExitProduct.Where(x => x.StatusType == ConstantHelpers.ESTADO.SALIDA && x.ProductBrandId == model.ProductBrandId).Sum(x => x.Amount);

                    StockProduct stockProduct = new StockProduct();
                    if (context.StockProduct.FirstOrDefault(x => x.ProductBrandId == model.ProductBrandId) != null)
                    {
                        stockProduct = context.StockProduct.FirstOrDefault(x => x.ProductBrandId == model.ProductBrandId);
                    }
                    else {
                        context.StockProduct.Add(stockProduct);
                    }
                    
                    stockProduct.Amount = (entradasProducto.HasValue ? Convert.ToDecimal(entradasProducto) : Convert.ToDecimal(0)) - (salidasProducto.HasValue ? Convert.ToDecimal(salidasProducto) : Convert.ToDecimal(0));
                    stockProduct.Status = ConstantHelpers.ESTADO.ACTIVO;
                    stockProduct.ProductBrandId = model.ProductBrandId;
                    context.SaveChanges();

                    ts.Complete();

                    PostMessage(MessageType.Success, "Entrada de producto Guardado");
                    return RedirectToAction("ListStockProduct", "StockProduct");
                }
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
    }
}