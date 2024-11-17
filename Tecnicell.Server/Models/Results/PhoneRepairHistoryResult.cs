namespace Tecnicell.Server.Models.Results
{
    public class PhoneRepairHistoryResult
    {
        public string? Imei {  get; set; }
        public string? Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Usuario { get; set; }
        public string? Sucursal { get; set; }
        public string? Accion { get; set; }
        public string? Descripción { get; set; }
        public string? Moneda { get; set; }
        public decimal? Precio { get; set; }
        public DateTime? Garantía { get; set; }
    }
}
