namespace Orenes.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }

        public string Password { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } // Relación uno a muchos

    }
}
