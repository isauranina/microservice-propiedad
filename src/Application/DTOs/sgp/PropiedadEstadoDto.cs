using System.Text.Json.Serialization;

namespace Application.DTOs.sgp
{
	public class PropiedadEstadoDto
    {
        public long num_sec { get; set; }
        public long nsec_propiedad { get; set; }
        public long nsec_estado { get; set; }
        public string? fecha_inicio { get; set; }
        public string? fecha_fin { get; set; }
        [JsonIgnore]
        public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
