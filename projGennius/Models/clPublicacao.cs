using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace projGennius.Models
{
    public class clPublicacao
    {

        public string codPubli { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "A Categoria é obrigatória!")]
        public string catPubli { get; set; }

        [Display(Name = "Descrição do serviço")]
        [Required(ErrorMessage = "A Descrição é obrigatória!")]
        public string descricao { get; set; }

        public string codUsu { get; set; }
        
        public string codPres { get; set; }

        public string empresaCLi{ get; set; }

        public string situacaoPubli { get; set; }

        public string dataPubli { get; set; }
    }
}