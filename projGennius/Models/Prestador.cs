using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static projGennius.Models.Usuario;
using System.Xml.Linq;



namespace projGennius.Models
{
    public class Prestador : Usuario
    {
         public int codPres { get; set; }

         [Display(Name = "GitHub")]
         [Required(ErrorMessage = "O Github é obrigatório")]
         public string gitPres { get; set; }
    }
}
