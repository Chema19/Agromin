using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sagromin.Controllers;
using Sagromin.Helpers;
using Sagromin.Models;

namespace Sagromin.ViewModels.Sale
{
    public class _AddEditSaleViewModel
    {
        public Int32? SaleId { set; get; }

        [Display(Name = "Local : ")]
        [Required(ErrorMessage = "El campo local es requerido")]
        public String NameLocal { set; get; }

        [Display(Name = "Usuario : ")]
        [Required(ErrorMessage = "El campo usuario es requerido")]
        public String NameUser { set; get; }   

        [Display(Name = "Cliente : ")]
        [Required(ErrorMessage = "El campo cliente es requerido")]
        public Int32? CustomerId { set; get; }
        public Int32? CustomerIdHidden { set; get; }
        public List<Sagromin.Models.Customer> LstCustomer { set; get; }

        [Display(Name = "Numero Comprobante : ")]
        [Required(ErrorMessage = "El campo comprobante es requerido")]
        public String CodeVoucher { set; get; }

        public List<SaleDetail> LstSaleDetail { set; get; }

        public _AddEditSaleViewModel() {
            LstCustomer = new List<Sagromin.Models.Customer>();
            LstSaleDetail = new List<SaleDetail>();
        }

        public void Fill(CargarDatosContext dataContext, Int32? saleId) {
           

            this.SaleId = saleId;
            if (!SaleId.HasValue)
            {
                var localId = dataContext.session.GetLocalId();
                var userId = dataContext.session.GetUserId();
                var listSale = dataContext.context.Sale.Where(x => x.LocalId == localId).ToList();
                CodeVoucher = NumberVoucher(listSale);
                NameLocal = dataContext.context.Local.FirstOrDefault(x => x.LocalId == localId).Name;
                NameUser = dataContext.context.User.FirstOrDefault(x => x.UserId == userId).Names + " " + dataContext.context.User.FirstOrDefault(x => x.UserId == userId).Last_Names;
            }
            else {
                Sagromin.Models.Sale sale = dataContext.context.Sale.FirstOrDefault(x => x.SaleId == saleId);
                LstSaleDetail = dataContext.context.SaleDetail.Where(x => x.SaleId == saleId).ToList();
                CodeVoucher = sale.CodeVoucher;
                NameLocal = sale.Local.Name;
                NameUser = sale.User.Names;
                CustomerId = sale.CustomerId;
                CustomerIdHidden = sale.CustomerId;
            }
        }
        public String NumberVoucher(List<Sagromin.Models.Sale> listSale) {
            String code = "N°";
            Sagromin.Models.Sale lastSale = listSale.OrderByDescending(x => x.SaleId).FirstOrDefault();
            if (listSale.Count() > 0)
            {
                Int32 numberVoucher = listSale.Count() + 1;
                for (int i = numberVoucher.ToString().Length; i < 8; i++) {
                    code = code + "0";
                }
                code = code + numberVoucher.ToString();
            }
            else {
                code = code + "00000001";
            }
            return code;
        }
    }
}