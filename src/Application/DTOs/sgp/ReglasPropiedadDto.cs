using System.Text.Json.Serialization;

namespace Application.DTOs.sgp
{
	public class ReglasPropiedadDto
    {
		public long num_sec { get; set; }
		public long nsec_propiedad { get; set; }
		public string? descripcion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
