using PagedList;
using Sagromin.Controllers;
using Sagromin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sagromin.ViewModels.StockProduct
{
    public class ListStockProductViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.StockProduct> LstStockProduct { set; get; }
        public void Fill(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            var query = dataContext.context.StockProduct.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO);
            LstStockProduct = query.OrderBy(x => x.StockProductId).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}