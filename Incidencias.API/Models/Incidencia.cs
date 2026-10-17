namespace Incidencias.API.Models
{
    /// <summary>
    /// Esto es un ticket
    /// </summary>
    public class Incidencia : BaseModel
    {
        public Incidencia()
        {
            Notas = new HashSet<NotaIncidencia>();
        }
        public string Titulo { get; set; }

        public string Descripcion { get; set; }
        public EstadoIncidencia Estado { get; set; }
        public PrioridadIncidencia Prioridad { get; set; }

        /// <summary>
        /// Llave foranea de usuario 
        /// </summary>
        public Guid CreadoPorId { get; set; }

        /// <summary>
        /// Propiedad de navegación de 1 a Muchos, me dice quién creó la incidencia
        /// </summary>
        public ApplicationUser CreadoPor { get; set; }

    public ICollection<NotaIncidencia> Notas { get; set; }


    }

    public enum EstadoIncidencia
    {
        Abierto,
        Trabajado,
        enRevision,
        Aprobado,
        Rechazado,
        aceptado,
        cerrado

    }

    public enum PrioridadIncidencia
    {
        Baja,
        Media,
        Alta,
        Urgente

    }
}
