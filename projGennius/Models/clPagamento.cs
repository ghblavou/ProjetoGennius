using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projGennius.Models
{
    public class clPagamento
    {
        public int codpag { get; set; }
        public string NomeCartao { get; set; }
        public string codUsu { get; set; }
        public string numeroCartao { get; set; }
        public string validadeCartao { get; set; }
        public string anoCartao { get; set; }
        public string mesCartao { get; set; }
        public string cvvCartao { get; set; }
        public int codPedido { get; set; }
        public int codProd { get; set; }
    }
}