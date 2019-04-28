using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sagromin.Helpers;
using Sagromin.Models;
using Sagromin.Controllers;
using PagedList;

namespace Sagromin.ViewModels.User
{
    public class ListUserViewModel
    {
        public Int32 p { set; get; }
        public IPagedList<Sagromin.Models.User> LstUser { set; get; }
        public void Fill(CargarDatosContext dataContext,Int32? p) {
            var time = DateTime.Now.TimeOfDay.Subtract(new TimeSpan(0, 7, 0));
            this.p = p ?? 1;
            var query = dataContext.context.User.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO);
            LstUser = query.OrderBy(x=>x.UserId).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}