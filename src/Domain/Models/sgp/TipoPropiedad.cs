using System.Text.Json.Serialization;

namespace Domain.Models.sgp
{
	public class TipoPropiedad
    {
        public long num_sec { get; set; }
		public string? nombre_tipo { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
