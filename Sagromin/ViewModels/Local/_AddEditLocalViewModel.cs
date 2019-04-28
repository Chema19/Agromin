using Sagromin.Controllers;
using Sagromin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sagromin.ViewModels.Local
{
    public class _AddEditLocalViewModel
    {
        public Int32? LocalId { set; get; }

        [Display(Name = "Nombre : ")]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public String Name { set; get; }

        [Display(Name = "Número de Celular : ")]
        [Required(ErrorMessage = "El campo celular es requerido")]
        public String Phone { set; get; }

        [Display(Name = "Dirección : ")]
        [Required(ErrorMessage = "El campo dirección es requerido")]
        public String Address { set; get; }

        [Display(Name = "Departamento : ")]
        [Required(ErrorMessage = "El campo departamento es requerido")]
        public Int32? DepartmentId { set; get; }
        public List<Department> LstDepartment { set; get; }

        [Display(Name = "Provincia : ")]
        [Required(ErrorMessage = "El campo provincia es requerido")]
        public Int32? ProvinceId { set; get; }
        public List<Province> LstProvince { set; get; }

        [Display(Name = "Distrito : ")]
        [Required(ErrorMessage = "El campo distrito es requerido")]
        public Int32? DistrictId { set; get; }
        public List<District> LstDistrict { set; get; }

        public Int32? DepartmentIdHidden { set; get; }
        public Int32? ProvinceIdHidden { set; get; }
        public Int32? DistrictIdHidden { set; get; }

        public _AddEditLocalViewModel()
        {
            LstDepartment = new List<Department>();
            LstDistrict = new List<District>();
            LstProvince = new List<Province>();
        }

        public void Fill(CargarDatosContext datosContext, Int32? userId)
        {
            this.LocalId = userId;
            if (this.LocalId.HasValue)
            {
                var local = datosContext.context.Local.FirstOrDefault(x => x.LocalId == this.LocalId);
                this.Name = local.Name;
                this.Phone = local.Phone;
                this.Address = local.Address;
                this.DistrictId = local.DistrictId;
                this.ProvinceId = local.District.ProvinceId;
                var provincia = datosContext.context.Province.FirstOrDefault(x => x.ProvinceId == this.ProvinceId);
                this.DepartmentId = provincia.DepartmentId;
                this.DepartmentIdHidden = local.District.Province.DepartmentId;
                this.ProvinceIdHidden = local.District.ProvinceId;
                this.DistrictIdHidden = local.DistrictId;
            }
        }
    }
}