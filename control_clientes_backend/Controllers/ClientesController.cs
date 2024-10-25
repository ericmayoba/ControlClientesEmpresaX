using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using control_clientes_backend.Data;
using control_clientes_backend.Models;
using control_clientes_backend.Models.DTOs; 

namespace control_clientes_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClientes()
        {
            var clientes = await _context.Clientes.Include(c => c.Direcciones).ToListAsync();

            var clienteDtos = clientes.Select(c => new ClienteDto
            {
                ClienteId = c.ClienteId,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono,
                OtrosDatos = c.OtrosDatos,
                Direcciones = c.Direcciones.Select(d => new DireccionDto
                {
                    DireccionId = d.DireccionId,
                    ClienteId = d.ClienteId,
                    Calle = d.Calle,
                    Sector = d.Sector,
                    Provincia = d.Provincia,
                    Pais = d.Pais
                }).ToList()
            }).ToList();

            return Ok(clienteDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Direcciones)
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
            {
                return NotFound();
            }

            var clienteDto = new ClienteDto
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                OtrosDatos = cliente.OtrosDatos,
                Direcciones = cliente.Direcciones.Select(d => new DireccionDto
                {
                    DireccionId = d.DireccionId,
                    ClienteId = d.ClienteId,
                    Calle = d.Calle,
                    Sector = d.Sector,
                    Provincia = d.Provincia,
                    Pais = d.Pais
                }).ToList()
            };

            return Ok(clienteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteDto clienteDto)
        {
            if (id != clienteDto.ClienteId)
            {
                return BadRequest();
            }

            var cliente = await _context.Clientes.Include(c => c.Direcciones).FirstOrDefaultAsync(c => c.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            // Actualiza las propiedades del cliente
            cliente.Nombre = clienteDto.Nombre;
            cliente.Email = clienteDto.Email;
            cliente.Telefono = clienteDto.Telefono;
            cliente.OtrosDatos = clienteDto.OtrosDatos;

            // Limpia las direcciones existentes y agrega las nuevas
            cliente.Direcciones.Clear(); // Limpia las direcciones existentes

            foreach (var direccionDto in clienteDto.Direcciones)
            {
                var direccion = new Direccion
                {
                    DireccionId = direccionDto.DireccionId, // Asegúrate de asignar un ID si es necesario
                    ClienteId = cliente.ClienteId,
                    Calle = direccionDto.Calle,
                    Sector = direccionDto.Sector,
                    Provincia = direccionDto.Provincia,
                    Pais = direccionDto.Pais
                };
                cliente.Direcciones.Add(direccion); // Agrega la nueva dirección
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> PostCliente(ClienteDto clienteDto)
        {
            var cliente = new Cliente
            {
                Nombre = clienteDto.Nombre,
                Email = clienteDto.Email,
                Telefono = clienteDto.Telefono,
                OtrosDatos = clienteDto.OtrosDatos,
                Direcciones = clienteDto.Direcciones.Select(d => new Direccion
                {
                    Calle = d.Calle,
                    Sector = d.Sector,
                    Provincia = d.Provincia,
                    Pais = d.Pais
                }).ToList() // Conversión de DireccionDto a Direccion
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            clienteDto.ClienteId = cliente.ClienteId; // Establecer el ID generado

            return CreatedAtAction("GetCliente", new { id = cliente.ClienteId }, clienteDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}


