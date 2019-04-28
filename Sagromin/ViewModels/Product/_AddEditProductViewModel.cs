using Sagromin.Controllers;
using Sagromin.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sagromin.ViewModels.Product
{
    public class _AddEditProductViewModel
    {
        public Int32? ProductId { set; get; }

        [Display(Name = "Nombres : ")]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public String Name { set; get; }
       

        public void Fill(CargarDatosContext datosContext, Int32? ProductId)
        { 
            this.ProductId = ProductId;
            if (this.ProductId.HasValue)
            {
                var Product = datosContext.context.Product.FirstOrDefault(x => x.ProductId == this.ProductId);
                this.Name = Product.Name;
            }
        }
    }
}