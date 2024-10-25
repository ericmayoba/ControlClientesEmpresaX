namespace control_clientes_backend.Models.DTOs;

public class DireccionDto
{
    public int DireccionId { get; set; } 
    public int ClienteId { get; set; } 

    public string? Calle { get; set; }
    public string? Sector { get; set; }
    public string? Provincia { get; set; }
    public string? Pais { get; set; }

    public virtual ClienteDto? Cliente { get; set; }



}