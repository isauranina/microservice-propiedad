using System.Text.Json.Serialization;

namespace Application.DTOs.sgp
{
	public class PaisDto
    {
		public long num_sec { get; set; }
		public string? descripcion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
