//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebGestion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DireccionesFacturacion
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int Activa { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string CP { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public int LocalidadId { get; set; }
        public int IdiomaId { get; set; }
        public int EstadoId { get; set; }
        public string Observaciones { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaBaja { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Estados Estados { get; set; }
        public virtual Localidades Localidades { get; set; }
        public virtual Idiomas Idiomas { get; set; }
    }
}