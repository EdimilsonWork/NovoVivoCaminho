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

    public partial class Equipes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipes()
        {
            this.Membros = new HashSet<Membros>();
        }
    
        public int ID { get; set; }
        [StringLength(250, ErrorMessage = "Tamanho m�ximo de 250 caracteres.")]
        public string Nome { get; set; }
        public int IDIgreja { get; set; }
        public bool Ativo { get; set; }
    
        public virtual Igrejas Igrejas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membros> Membros { get; set; }
    }
}
