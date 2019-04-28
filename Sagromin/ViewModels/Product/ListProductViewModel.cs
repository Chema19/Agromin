using PagedList;
using Sagromin.Controllers;
using Sagromin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sagromin.ViewModels.Product
{
    public class ListProductViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.Product> LstProduct { set; get; }
        public void Fill(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            var query = dataContext.context.Product.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).AsQueryable();
            LstProduct = query.OrderBy(x => x.ProductId).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}