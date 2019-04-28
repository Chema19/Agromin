using PagedList;
using Sagromin.Controllers;
using Sagromin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sagromin.ViewModels.Local
{
    public class ListLocalViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.Local> LstLocal { set; get; }
        public void Fill(CargarDatosContext dataContext, Int32? p)
        {
            var time = DateTime.Now.TimeOfDay.Subtract(new TimeSpan(0, 7, 0));
            this.p = p ?? 1;
            var query = dataContext.context.Local.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO);
            LstLocal = query.OrderBy(x => x.LocalId).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}