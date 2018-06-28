using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NovoVivoCaminho.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        [Required(ErrorMessage = "Informe a IGREJA do usuário")]
        public int IDIgreja { get; set; }

        [Required(ErrorMessage = "Informe o NOME do usuário")]
        [MaxLength(100, ErrorMessage = "O NOME deve ter até 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o LOGIN do usuário.")]
        [MaxLength(50, ErrorMessage = "O LOGIN deve ter até 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a SENHA do usuário")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A SENHA deve ter no mínimo 6 caracteres")]
        [MaxLength(100, ErrorMessage = "A SENHA deve ter até 100 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme a SENHA do usuário")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [MinLength(6, ErrorMessage = "A SENHA deve ter no mínimo 6 caracteres")]
        [MaxLength(100, ErrorMessage = "A SENHA deve ter até 100 caracteres")]
        [Compare(nameof(Senha), ErrorMessage = "A SENHA e a CONFIRMAÇÃO não são iguais")]
        public string ConfirmacaoSenha { get; set; }

        public bool Ativo { get; set; }
    }
}