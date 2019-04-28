using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sagromin.Controllers;
using Sagromin.Models;
using PagedList;
using Sagromin.Helpers;

namespace Sagromin.ViewModels.StockProduct
{
    public class ListEntryExitProductViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.EntryExitProduct> LstEntryExitProduct { set; get; }
        public void Fill(CargarDatosContext dataContext, Int32? p, Int32? ProductBrandId) {
            this.p = p ?? 1;
            var query = dataContext.context.EntryExitProduct.AsQueryable();
            query = query.Where(x => x.ProductBrandId == ProductBrandId);
            LstEntryExitProduct = query.OrderByDescending(x => x.Creation_Date).ToPagedList(this.p,ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}