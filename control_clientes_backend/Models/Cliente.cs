using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace control_clientes_backend.Models;

public class Cliente
{
    public int ClienteId { get; set; } // Primary Key
    public string? Nombre { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string? OtrosDatos { get; set; }

    // Relación uno a muchos con la entidad Direccion
    public virtual required ICollection<Direccion> Direcciones { get; set; }
}
