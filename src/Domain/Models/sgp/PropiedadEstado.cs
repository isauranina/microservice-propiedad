using System.Text.Json.Serialization;

namespace Domain.Models.sgp
{
	public class PropiedadEstado
    {
        public long num_sec { get; set; }
		public long nsec_propiedad { get; set; }
		public long nsec_estado { get; set; }
        public string? fecha_inicio { get; set; }
        public string? fecha_fin { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
