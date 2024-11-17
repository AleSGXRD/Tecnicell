namespace Tecnicell.Server.Models.Results
{
    public class PhoneRepairViewResult
    {
        public string? Imei { get; set; }
        public string? Nombre { get; set; }
        public string? Marca { get; set; }
        public decimal? Precio { get; set; }
        public string? Cliente_Nombre { get; set; }
        public string? Cliente_CI { get; set; }
        public string? Cliente_Teléfono { get; set; }
        public string? Estado { get; set; }
        public DateTime? Fecha_Ultima_Accion { get; set; }
        public string? Descripción { get; set; }
    }
}
