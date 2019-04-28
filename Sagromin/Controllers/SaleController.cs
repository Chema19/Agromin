using Sagromin.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sagromin.Models;
using Sagromin.Helpers;
using System.Transactions;

namespace Sagromin.Controllers
{
    public class SaleController : BaseController
    {
        // GET: Sale
        public ActionResult ListSale(Int32? p)
        {
            var vm = new ListSaleViewModel();
            vm.Fill(CargarDatosContext(), p);
            return View(vm);
        }

        public ActionResult _AddEditSale(Int32? SaleId)
        {
            var vm = new _AddEditSaleViewModel();
            vm.Fill(CargarDatosContext(), SaleId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult _AddEditSale(_AddEditSaleViewModel model, FormCollection form)
        {
            try {
                using (TransactionScope ts = new TransactionScope()) {
                    Sale sale = new Sale();
                    if (model.SaleId.HasValue)
                    {
                        sale = context.Sale.FirstOrDefault(x => x.SaleId == model.SaleId);
                        var saleDetailEdit = context.SaleDetail.Where(x => x.SaleId == sale.SaleId).ToList();
                        var entryExitProductEdit = context.EntryExitProduct.Where(x => x.SaleId == sale.SaleId).ToList();
                        foreach (var item in saleDetailEdit) {
                            var stockProductEdit = new StockProduct();
                            stockProductEdit = context.StockProduct.FirstOrDefault(x => x.ProductBrandId == item.ProductBrandId);
                            stockProductEdit.Amount = stockProductEdit.Amount + item.Amount;
                        }
                        saleDetailEdit.ForEach(x => context.SaleDetail.Remove(x));
                        entryExitProductEdit.ForEach(x => context.EntryExitProduct.Remove(x));
                    }
                    else
                    {
                        context.Sale.Add(sale);
                        sale.Status = ConstantHelpers.ESTADO.ACTIVO;
                        sale.StatusPayment = ConstantHelpers.ESTADO.PREVENTA;
                        sale.StatusDelivery = ConstantHelpers.ESTADO.NOENTREGADO;
                        sale.Creation_Date = DateTime.Now;
                    }

                    sale.Update_Date = DateTime.Now;
                    sale.LocalId = Session.GetLocalId();
                    sale.UserId = Session.GetUserId();
                    sale.CodeVoucher = model.CodeVoucher;
                    sale.CustomerId = model.CustomerId;
                    context.SaveChanges();

                    Decimal? totalGeneral = 0;
                    foreach (var key in form.AllKeys.Where(x => x.StartsWith("product-")).ToList())
                    {
                        var num = key.Split('-');
                        var keyProduct = "product-" + num[1];
                        var keyQuantity = "quantity-" + num[1];
                        var keyUnitPrice = "unitprice-" + num[1];
                        var keyimport = "import-" + num[1];

                        var productBrandId = Convert.ToInt32(form[keyProduct]);

                        SaleDetail saleDetail = new SaleDetail();
                        context.SaleDetail.Add(saleDetail);
                        saleDetail.SaleId = sale.SaleId;
                        saleDetail.ProductBrandId = Convert.ToInt32(form[keyProduct]);
                        saleDetail.Amount = Convert.ToInt32(form[keyQuantity]);
                        saleDetail.Price = Convert.ToDecimal(form[keyUnitPrice]);
                        saleDetail.Total = saleDetail.Price * saleDetail.Amount;
                        saleDetail.Status = ConstantHelpers.ESTADO.ACTIVO;

                        EntryExitProduct entryExitProduct = new EntryExitProduct();
                        context.EntryExitProduct.Add(entryExitProduct);
                        entryExitProduct.Amount = Convert.ToInt32(form[keyQuantity]);
                        entryExitProduct.Creation_Date = DateTime.Now;
                        entryExitProduct.StatusType = ConstantHelpers.ESTADO.SALIDA;
                        entryExitProduct.UserId = Session.GetUserId();
                        entryExitProduct.ProductBrandId = Convert.ToInt32(form[keyProduct]);
                        entryExitProduct.SaleId = sale.SaleId;

                        StockProduct stockProduct = new StockProduct();
                        stockProduct = context.StockProduct.FirstOrDefault(x => x.ProductBrandId == productBrandId);
                        stockProduct.Amount -= Convert.ToInt32(form[keyQuantity]); ;

                        totalGeneral += saleDetail.Total;
                    }
                    sale.General_Price = totalGeneral;
                    context.SaveChanges();
                    ts.Complete();
                }

                PostMessage(MessageType.Success, "Venta Realizada");
                return RedirectToAction("ListSale");
            } catch (Exception ex) {
                return RedirectToAction("ListSale");
            }
        }
        [HttpPost]
        public JsonResult _ConfirmPaymentSale(Int32? Id) {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var sale = context.Sale.FirstOrDefault(x => x.SaleId == Id);
                        sale.StatusPayment = ConstantHelpers.ESTADO.CANCELADO;
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
        [HttpPost]
        public JsonResult _ConfirmDeliverySale(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var sale = context.Sale.FirstOrDefault(x => x.SaleId == Id);
                        sale.StatusDelivery = ConstantHelpers.ESTADO.ENTREGADO;
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