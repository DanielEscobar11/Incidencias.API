using Incidencias.API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Incidencias.API.Dto
{
    public class IncidenciaDto
    {
        public string Titulo { get; set; }

        public string Descripcion { get; set; }
        public EstadoIncidencia Estado { get; set; }
        public PrioridadIncidencia Prioridad { get; set; }

        /// <summary>
        /// Llave foranea de usuario 
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public Guid CreadoPorId { get; set; }


    }
}
