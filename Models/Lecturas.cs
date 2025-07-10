namespace LecturasApi.Models
{
    public class Lectura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public int MedidorId { get; set; }
        public Medidor Medidor { get; set; }
    }
}
