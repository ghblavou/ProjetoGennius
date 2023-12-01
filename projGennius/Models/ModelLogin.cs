using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace projGennius.Models
{
    public class ModelLogin
    {

        public string codUsu { get; set; }


        [Required(ErrorMessage = "O login é obrigatório")]
        [Display(Name = "Login")]
        [RegularExpression(@"^[a-z]+\.[a-z]+$", ErrorMessage = "Informe um nome válido!\nEx: 'nome.sobrenome'")]
        public string login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres")]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Required(ErrorMessage = "Por favor, repita a senha")]
        [Display(Name = "Repita a senha")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 10 caracteres")]
        [Compare("senha", ErrorMessage = "As senhas não coincidem")]
        public string RepetirSenha { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome")]
        [RegularExpression(@"^[a-zA-ZáéíóúâêîôûàèìòùãõäëïöüçÁÉÍÓÚÂÊÎÔÛÀÈÌÒÙÃÕÄËÏÖÜÇ&' .-]+$", ErrorMessage = "Informe somente letras.")]
        public string nome { get; set; }

        public string tipo { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [Display(Name = "CPF")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Informe 11 números.")]
        public string cpf { get; set; }


        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Display(Name = "Telefone")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Informe 11 números.")]
        public string telefone { get; set; }

        public string sta { get; set; }

    }
}

