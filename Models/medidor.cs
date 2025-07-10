namespace LecturasApi.Models
{
    public class Medidor
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Lectura> Lecturas { get; set; }
    }
}
