using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace projGennius.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "O email é obrigatório")]
            [Display(Name = "Email")]
            [RegularExpression(@"^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$", ErrorMessage = "Informe um email válido! Ex: 'exemplo@email.com'")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória")]
            [DataType(DataType.Password)]
            [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 10 caracteres")]
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
            public string nome { get; set; }

            public string tipo { get; set; }

            [Required(ErrorMessage = "O CPF é obrigatório")]
            [Display(Name = "CPF")]
            public string cpf { get; set; }


            [Required(ErrorMessage = "O telefone é obrigatório")]
            [Display(Name = "Telefone")]
            public string telefone { get; set; }

            
    }
    }

