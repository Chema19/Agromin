using PagedList;
using Sagromin.Controllers;
using Sagromin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sagromin.ViewModels.Brand
{
    public class ListBrandViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.Brand> LstBrand { set; get; }
        public void Fill(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            var query = dataContext.context.Brand.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO);
            LstBrand = query.OrderBy(x => x.BrandId).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}