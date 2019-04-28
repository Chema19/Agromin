using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sagromin.Helpers;
using Sagromin.Models;
using Sagromin.Controllers;
using PagedList;

namespace Sagromin.ViewModels.ProductBrand
{
    public class ListProductBrandViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.ProductBrand> LstProductBrand { set; get; }
        public void Fill(CargarDatosContext dataContext,Int32? p) {
            this.p = p ?? 1;
            var query = dataContext.context.ProductBrand.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO);
            LstProductBrand = query.OrderBy(x=>x.ProductBrandId).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}