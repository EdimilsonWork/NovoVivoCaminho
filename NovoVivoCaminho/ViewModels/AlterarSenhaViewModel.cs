using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NovoVivoCaminho.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe sua SENHA atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        [MinLength(6, ErrorMessage = "A SENHA deve ter no mínimo 6 caracteres")]
        [MaxLength(100, ErrorMessage = "A SENHA deve ter até 100 caracteres")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Informe sua NOVA SENHA atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova atual")]
        [MinLength(6, ErrorMessage = "A SENHA deve ter no mínimo 6 caracteres")]
        [MaxLength(100, ErrorMessage = "A SENHA deve ter até 100 caracteres")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme sua NOVA SENHA")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [MinLength(6, ErrorMessage = "A SENHA deve ter no mínimo 6 caracteres")]
        [MaxLength(100, ErrorMessage = "A SENHA deve ter até 100 caracteres")]
        [Compare(nameof(NovaSenha), ErrorMessage = "A SENHA e a CONFIRMAÇÃO não são iguais")]
        public string ConfirmarSenha { get; set; }
    }
}