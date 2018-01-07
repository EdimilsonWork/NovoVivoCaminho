//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NovoVivoCaminho.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuarios
    {
        public int ID { get; set; }
        public int IDIgreja { get; set; }
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Informe o nome do usu�rio", AllowEmptyStrings = false)]
        public string Login { get; set; }
        [Required(ErrorMessage = "Informe a senha do usu�rio", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Senha { get; set; }
        public string Nome { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DataCriacao { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
    
        public virtual Igrejas Igrejas { get; set; }
    }
}
