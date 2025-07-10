namespace LecturasApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        public ICollection<Medidor> Medidor { get; set; }

    }
} 