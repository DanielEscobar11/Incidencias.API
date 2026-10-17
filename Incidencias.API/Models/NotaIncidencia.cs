namespace Incidencias.API.Models
{
    public class NotaIncidencia : BaseModel
    {
        public string Nota { get; set; }
        public Guid CreadoPorId { get; set; }
        public ApplicationUser CreadoPor { get; set; }
        public DateTime FechaNota { get; set; }
        public int IncidenciaId { get; set; }
        public Incidencia Incidencia{ get; set; }

    }
}
