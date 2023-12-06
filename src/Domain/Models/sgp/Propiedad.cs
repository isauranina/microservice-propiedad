using System.Text.Json.Serialization;

namespace Domain.Models.sgp
{
	public class Propiedad
    {
        public long num_sec { get; set; }
		public string? descripcion { get; set; }
		public string? direccion { get; set; }
		public bool esverificado { get; set; }
		public long nsec_tipo_propiedad { get; set; }
		public long nsec_ciudad { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
    public class VerificarPropiedad
    {
        public long num_sec { get; set; }        
        public bool esverificado { get; set; }
       
    }
}
