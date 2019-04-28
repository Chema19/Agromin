using Sagromin.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sagromin.Controllers
{
    public class ServiceController : BaseController
    {
        #region Department-Province-District
        public JsonResult _GetDepartment(String filtro, Int32? parameterId, Int32? parameterId2)
        {
            List<ResultSelect2Logic> data = new List<ResultSelect2Logic>();
            var query = context.Department.AsQueryable();
            if (parameterId2.HasValue)
            {
                query = query.Where(x => x.DepartmentId == parameterId2);
            }
            else
            {
                query = query.Where(x => x.Name.Contains(filtro));
            }
           
            data = query.Select(x => new ResultSelect2Logic
            {
                id = x.DepartmentId,
                text = x.Name
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _GetProvince(String filtro, Int32? parameterId, Int32? parameterId2)
        {
            List<ResultSelect2Logic> data = new List<ResultSelect2Logic>();
            var query = context.Province.AsQueryable();
            if (parameterId2.HasValue)
            {
                query = query.Where(x => x.ProvinceId == parameterId2);
            }
            else
            {
                query = query.Where(x => x.Name.Contains(filtro) && x.DepartmentId == parameterId);
            }
            data = query.Select(x => new ResultSelect2Logic
            {
                id = x.ProvinceId,
                text = x.Name
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _GetDistrict(String filtro, Int32? parameterId, Int32? parameterId2)
        {
            List<ResultSelect2Logic> data = new List<ResultSelect2Logic>();
            var query = context.District.AsQueryable();
            if (parameterId2.HasValue)
            {
                query = query.Where(x => x.DistrictId == parameterId2);
            }
            else
            {
                query = query.Where(x => x.Name.Contains(filtro) && x.ProvinceId == parameterId);
            }
            data = query.Select(x => new ResultSelect2Logic
            {
                id = x.DistrictId,
                text = x.Name
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Customer
        public JsonResult _GetCustomer(String filtro, Int32? CustomerId)
        {
            List<ResultSelect2Logic> data = new List<ResultSelect2Logic>();
            var query = context.Customer.AsQueryable();

            if (CustomerId.HasValue)
            {
                query = query.Where(x => x.CustomerId == CustomerId);
            }
            else
            {
                query = query.Where(x => x.Names.Contains(filtro) || x.Last_Names.Contains(filtro) || x.Identity_Document.Contains(filtro));
            }

            data = query.Select(x => new ResultSelect2Logic
            {
                id = x.CustomerId,
                text = x.Names + " " + x.Last_Names + " - " + x.Identity_Document
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Product
        public JsonResult _GetProduct(String filtro, Int32? ProductId)
        {
            List<SelectProducto> data = new List<SelectProducto>();
            var query = context.ProductBrand.AsQueryable();


            if (ProductId.HasValue)
            {
                query = query.Where(x => x.ProductId == ProductId);
            }
            else {
                query = query.Where(x => x.Product.Name.Contains(filtro) || x.Brand.Name.Contains(filtro));
            }
            
            data = query.Select(x => new SelectProducto
            {
                id = x.ProductBrandId,
                text = x.Product.Name + " " + x.Brand.Name,
                quantity = context.StockProduct.FirstOrDefault(m => m.ProductBrandId == x.ProductBrandId).Amount

            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class SelectProducto {
        public Int32 id { get; set; }
        public String text { get; set; }
        public Decimal? quantity { set;get; }
    }
}