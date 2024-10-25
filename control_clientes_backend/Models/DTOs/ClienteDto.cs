

namespace control_clientes_backend.Models.DTOs;

public class ClienteDto
{
    public int ClienteId { get; set; } 
    public string? Nombre { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string? OtrosDatos { get; set; }

    public virtual List<DireccionDto>? Direcciones { get; set; }

}