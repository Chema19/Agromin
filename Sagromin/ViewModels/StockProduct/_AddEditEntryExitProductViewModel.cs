using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sagromin.Controllers;
using Sagromin.Helpers;

namespace Sagromin.ViewModels.StockProduct
{
    public class FormatProductBrand {
        public Int32? ProductBrandId { set; get; }
        public String Name { set; get; }
    }

    public class _AddEditEntryExitProductViewModel
    {
        public Int32? EntryExitProductId { set; get; }
        [Display(Name = "Cantidad de producto : ")]
        [Required(ErrorMessage = "El campo cantidad de producto es requerido")]
        public Decimal Amount { set; get; }
        [Display(Name = "Producto : ")]
        [Required(ErrorMessage = "El campo producto es requerido")]
        public Int32? ProductBrandId { set; get; }
        public List<FormatProductBrand> LstProductBrand { set; get; }

        public _AddEditEntryExitProductViewModel() {
            LstProductBrand = new List<FormatProductBrand>();
        }

        public void Fill(CargarDatosContext dataContext) {
            LstProductBrand = dataContext.context.ProductBrand.Where(x=>x.Status == ConstantHelpers.ESTADO.ACTIVO).Select(x=>new FormatProductBrand { ProductBrandId = x.ProductBrandId, Name = x.Product.Name +" - "+ x.Brand.Name }).ToList();
        }
    }
}