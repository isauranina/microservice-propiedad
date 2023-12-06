namespace Domain.Models.Data
{
	public class RespuestaListado<T>
	{
		public string? status { get; set; }
		public IEnumerable<T>? response { get; set; }
		public int total { get; set; }
	}
}
