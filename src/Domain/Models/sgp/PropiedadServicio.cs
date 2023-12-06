using System.Text.Json.Serialization;

namespace Domain.Models.sgp
{
	public class PropiedadServicio
    {
        public long num_sec { get; set; }
		public long nsec_propiedad { get; set; }
		public long nsec_servicio { get; set; }
		public string? descripcion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
