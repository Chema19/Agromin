using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sagromin.Controllers;
using Sagromin.Models;
using Sagromin.Helpers;

namespace Sagromin.ViewModels.ProductBrand
{
    public class _AddEditProductBrandViewModel
    {
        
        public Int32? ProductBrandId { set; get; }

        [Display(Name = "Precio : ")]
        [Required(ErrorMessage = "El campo precio es requerido")]
        public Decimal? Price { set; get; }

        [Display(Name = "Producto : ")]
        [Required(ErrorMessage = "El campo producto es requerido")]
        public Int32? ProductId { set; get; }
        public List<Sagromin.Models.Product> LstProduct { set; get; }

        [Display(Name = "Marca : ")]
        [Required(ErrorMessage = "El campo marca es requerido")]
        public Int32? BrandId { set; get; }
        public List<Sagromin.Models.Brand> LstBrand { set; get; }


        public _AddEditProductBrandViewModel() {
            LstProduct = new List<Sagromin.Models.Product>();
            LstBrand = new List<Sagromin.Models.Brand>();
        }

        public void Fill(CargarDatosContext datosContext, Int32? ProductBrandId) {
            
            LstProduct = datosContext.context.Product.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).ToList();
            LstBrand = datosContext.context.Brand.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).ToList();

            this.ProductBrandId = ProductBrandId;
            if (this.ProductBrandId.HasValue) {
                var ProductBrand = datosContext.context.ProductBrand.FirstOrDefault(x => x.ProductBrandId == this.ProductBrandId);
                this.ProductId = ProductBrand.ProductId;
                this.BrandId = ProductBrand.BrandId;
            }
        }
    }
}