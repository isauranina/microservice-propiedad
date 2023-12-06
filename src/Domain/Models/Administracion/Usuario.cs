using System.Text.Json.Serialization;

namespace Domain.Models.Administracion
{
    public class Usuario
    {
        [JsonIgnore]
        public long num_sec { get; set; }
        public string nombre { get; set; } = "";
        public string cuenta { get; set; } = "";

        public string contrasena { get; set; } = "";
        public int nsec_rol { get; set; }
        public string rol { get; set; } = "";
        [JsonIgnore]
        public byte[]? hash { set; get; }
        [JsonIgnore]
        public byte[]? salt { set; get; }

        public string? image { get; set; }
        public string? email { get; set; }
        public string? estado { get; set; }

        public string? genero { get; set; }
        public string? fecha_modificacion { get; set; }
    }
}
