using System.Text.Json.Serialization;

namespace Domain.Models.sgp
{
	public class Tabla
    {
        public int num_sec { get; set; }
		public string? nombre { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
