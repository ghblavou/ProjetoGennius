using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projGennius.Models
{
    public class clProduto
    {
        public string codProd { get; set; }
        public string codUsu { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "A Categoria é obrigatória!")]
        public string catProd { get; set; }

        [Display(Name = "Descrição do serviço")]
        [Required(ErrorMessage = "A Descrição é obrigatória!")]
        public string descricaoProd { get; set; }
        public string imagemProd { get; set; }

    }
}