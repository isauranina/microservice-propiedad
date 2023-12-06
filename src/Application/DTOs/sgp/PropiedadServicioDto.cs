using System.Text.Json.Serialization;

namespace Application.DTOs.sgp
{
	public class PropiedadServicioDto
    {
		public long num_sec { get; set; }
		public long nsec_propiedad { get; set; }
		public long nsec_servicio { get; set; }
		public string? descripcion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
