using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sagromin.Controllers;
using Sagromin.Models;
using Sagromin.Helpers;

namespace Sagromin.ViewModels.User
{
    public class Sex {
        public String SexId { set; get; }
        public String Name { set; get; }
        public Sex(){}
        public Sex(String SexId, String Name) {
            this.SexId = SexId;
            this.Name = Name;
        }
    }
    public class _AddEditUserViewModel
    {
        
        public Int32? UserId { set; get; }

        [Display(Name = "Nombres : ")]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public String Names { set; get; }

        [Display(Name = "Apellidos : ")]
        [Required(ErrorMessage = "El campo apellido es requerido")]
        public String LastNames { set; get; }

        [Display(Name = "Credencial : ")]
        [Required(ErrorMessage = "El campo credencial es requerido")]
        public String Credential { set; get; }

        [Display(Name = "Contraseña : ")]
        [Required(ErrorMessage = "El campo contraseña es requerido")]
        public String Password { set; get; }

        [Display(Name = "Sexo : ")]
        public String Sex { set; get; }
        public List<Sex> LstSex { set; get; }

        [Display(Name = "Dni / Ce : ")]
        [Required(ErrorMessage = "El campo dni/ce es requerido")]
        public String Identity_Document { set; get; }

        [Display(Name = "Email : ")]
        [Required(ErrorMessage = "El campo email es requerido")]
        public String Email { set; get; }

        [Display(Name = "Fecha de Nacimiento : ")]
        [Required(ErrorMessage = "El campo fecha de nacimiento es requerido")]
        public DateTime? Birthdate { set; get; }

        [Display(Name = "Número de Celular : ")]
        [Required(ErrorMessage = "El campo celular es requerido")]
        public String Phone { set; get; }

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

        [Display(Name = "Local : ")]
        [Required(ErrorMessage = "El campo local es requerido")]
        public Int32 LocalId { set; get; }
        public List<Sagromin.Models.Local> LstLocal { set; get; }

        public Int32? DepartmentIdHidden { set; get; }
        public Int32? ProvinceIdHidden { set; get; }
        public Int32? DistrictIdHidden { set; get; }


        public _AddEditUserViewModel() {
            LstDepartment = new List<Department>();
            LstDistrict = new List<District>();
            LstProvince = new List<Province>();
            LstLocal = new List<Sagromin.Models.Local>();
            LstSex = new List<Sex>();
        }

        public void Fill(CargarDatosContext datosContext, Int32? userId) {
            LstSex.Add(new Sex("M", "Masculino"));
            LstSex.Add(new Sex("F", "Femenino"));

            LstLocal = datosContext.context.Local.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).ToList();

            this.UserId = userId;
            if (this.UserId.HasValue) {
                var user = datosContext.context.User.FirstOrDefault(x => x.UserId == this.UserId);
                this.Names = user.Names;
                this.LastNames = user.Last_Names;
                this.Credential = user.Credential;
                this.Password = user.Password;
                this.Sex = user.Sex;
                this.DistrictId = user.DistrictId;
                this.Identity_Document = user.Identity_Document;
                this.Birthdate = user.Birthdate;
                this.Phone = user.Phone;
                this.LocalId = user.LocalId;
                this.Email = user.Email;
                this.ProvinceId = user.District.ProvinceId;
                this.DepartmentId = user.District.Province.DepartmentId;
                this.DepartmentIdHidden = user.District.Province.DepartmentId;
                this.ProvinceIdHidden = user.District.ProvinceId;
                this.DistrictIdHidden = user.DistrictId;

            }
        }
    }
}