using PagedList;
using Sagromin.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sagromin.Models;
using Sagromin.Helpers;

namespace Sagromin.ViewModels.Customer
{
    public class ListCustomerViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.Customer> LstCustomer { set; get; }
        public void Fill(CargarDatosContext dataContext, Int32? p)
        {
            var time = DateTime.Now.TimeOfDay.Subtract(new TimeSpan(0, 7, 0));
            this.p = p ?? 1;
            var query = dataContext.context.Customer.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO);
            LstCustomer = query.OrderBy(x => x.CustomerId).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}