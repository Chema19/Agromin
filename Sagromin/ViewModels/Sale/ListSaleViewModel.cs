using PagedList;
using Sagromin.Controllers;
using Sagromin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sagromin.ViewModels.Sale
{
    public class ListSaleViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.Sale> LstSale { set; get; }
        public void Fill(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            var localId = dataContext.session.GetLocalId();
            var query = dataContext.context.Sale.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO && x.LocalId == localId );
            LstSale = query.OrderByDescending(x => x.Creation_Date).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}