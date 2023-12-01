using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static projGennius.Models.ModelLogin;
using System.Xml.Linq;



namespace projGennius.Models
{
    public class clPrestador : ModelLogin
    {

         [Display(Name = "GitHub")]
         [Required(ErrorMessage = "O Github é obrigatório")]
         public string gitPres { get; set; }
    }
}
