using System.Text.Json.Serialization;

namespace Application.DTOs.sgp
{
	public class TipoPropiedadDto
    {
		public long num_sec { get; set; }
		public string? nombre_tipo { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
