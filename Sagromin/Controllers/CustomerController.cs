using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sagromin.ViewModels.Customer;
using System.Transactions;
using Sagromin.Models;
using Sagromin.Helpers;

namespace Sagromin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult ListCustomer(Int32? p)
        {
            var vm = new ListCustomerViewModel();
            vm.Fill(CargarDatosContext(), p);
            return View(vm);
        }

        public ActionResult _AddEditCustomer(Int32? CustomerId)
        {
            var vm = new _AddEditCustomerViewModel();
            vm.Fill(CargarDatosContext(), CustomerId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult _AddEditCustomer(_AddEditCustomerViewModel model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Customer customer = new Customer();
                    if (model.CustomerId.HasValue)
                    {
                        customer = context.Customer.FirstOrDefault(x => x.CustomerId == model.CustomerId);
                        customer.Update_Date = DateTime.Now;
                    }
                    else
                    {
                        context.Customer.Add(customer);
                        customer.Status = ConstantHelpers.ESTADO.ACTIVO;
                        customer.Creation_Date = DateTime.Now;
                        customer.Update_Date = DateTime.Now;
                    }

                    customer.Names = model.Names;
                    customer.Last_Names = model.LastNames;
                    customer.Sex = model.Sex;
                    customer.Identity_Document = model.Identity_Document;
                    customer.Email = model.Email;
                    customer.Birthdate = model.Birthdate;
                    customer.DistrictId = model.DistrictId;
                    customer.Phone = model.Phone;

                    context.SaveChanges();
                    ts.Complete();
                }
                PostMessage(MessageType.Success, "Cliente Guardado");
                return RedirectToAction("ListCustomer");
            }
            catch (Exception e)
            {
                return View(model);
            }

        }

        [HttpPost]
        public JsonResult _DeleteCustomer(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var user = context.Customer.FirstOrDefault(x => x.CustomerId == Id);
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