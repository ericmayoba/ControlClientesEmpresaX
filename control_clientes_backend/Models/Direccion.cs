using System.Text.Json.Serialization;

namespace control_clientes_backend.Models;
public class Direccion
{
    public int DireccionId { get; set; } 
    public int ClienteId { get; set; } 

    public string? Calle { get; set; }
    public string? Sector { get; set; }
    public string? Provincia { get; set; }
    public string? Pais { get; set; }

    public virtual Cliente? Cliente { get; set; }



}
