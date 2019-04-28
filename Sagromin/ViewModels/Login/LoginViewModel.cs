using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sagromin.ViewModels.Login
{
    public class LoginViewModel
    {
        [Display(Name = "Usuario : ")]
        public String Credential { set; get; }
        [Display(Name = "Contraseña : ")]
        public String Password { set; get; }
    }
}