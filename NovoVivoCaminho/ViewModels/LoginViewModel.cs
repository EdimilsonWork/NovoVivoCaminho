using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NovoVivoCaminho.ViewModels
{
    public class LoginViewModel
    {
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Informe o LOGIN do usuário.")]
        [MaxLength(50, ErrorMessage = "O LOGIN deve ter até 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a SENHA do usuário.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A SENHA deve ter no mínimo 6 caracteres")]
        [MaxLength(100, ErrorMessage = "A SENHA deve ter até 100 caracteres")]
        public string Senha { get; set; }
    }
}