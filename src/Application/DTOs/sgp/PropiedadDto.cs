using System.Text.Json.Serialization;

namespace Application.DTOs.sgp
{
	public class PropiedadDto
    {
		public long num_sec { get; set; }
		public string? descripcion { get; set; }
		public string? direccion { get; set; }
		public bool esverificado { get; set; }
		public long nsec_tipo_propiedad { get; set; }
		public long nsec_ciudad { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
    public class ListaDto
    {
        public long id { get; set; }
        public string? descripcion { get; set; }
        public string? direccion { get; set; }
        public bool esverificado { get; set; }
        public string? tipo_propiedad { get; set; }
        public string? ciudad { get; set; }
        public decimal precio { get; set; }
       

       
    }
}
