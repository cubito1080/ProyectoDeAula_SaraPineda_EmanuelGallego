//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoDeAula_SaraPineda_EmanuelGallego.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbAgua
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbAgua()
        {
            this.tbFactura = new HashSet<tbFactura>();
        }
    
        public int IdAgua { get; set; }
        public int IdCliente { get; set; }
        public double PromedioConsumo { get; set; }
        public double ConsumoActual { get; set; }
        public int PeriodoConsumo { get; set; }
    
        public virtual tbCliente tbCliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFactura> tbFactura { get; set; }
    }
}
