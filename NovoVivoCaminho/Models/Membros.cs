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
    
    public partial class Membros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Membros()
        {
            this.Dizimos = new HashSet<Dizimos>();
        }
    
        public int ID { get; set; }
        public int IDIgreja { get; set; }
        public Nullable<int> IDEquipe { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string DDD { get; set; }
        public string Fone { get; set; }
        public string EstadoCivil { get; set; }
        public bool Batizado { get; set; }
        public Nullable<System.DateTime> DataDeNascimento { get; set; }
        public Nullable<System.DateTime> MembroDesde { get; set; }
    
        public virtual Equipes Equipes { get; set; }
        public virtual Igrejas Igrejas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dizimos> Dizimos { get; set; }
    }
}
