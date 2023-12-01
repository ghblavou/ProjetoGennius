using projGennius.Models;
using projGennius.Dados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace projGennius.Models
{
    public class Cliente : Usuario
    {
        public int codCli { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        [Display(Name = "Empresa")]
        public string empresaCli { get; set; }
    }
}
