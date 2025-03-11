using Microsoft.AspNetCore.Mvc;
using SistemaReservasAPI.Models;

namespace SistemaReservasAPI.Controllers;

[ApiController]
[Route("api/reservas")]
public class ReservasController : ControllerBase
{
    private static List<Reserva> reservas = new List<Reserva>();
    private static int nextId = 1;

    // Obtengo todas las reservas
    [HttpGet]
    public ActionResult<IEnumerable<Reserva>> GetReservas()
    {
        var reservasFormateadas = reservas.Select(r => new
        {
            r.Id,
            r.Cliente,
            r.Servicio,
            Fecha = r.Fecha.ToString("dd-MM-yyyy"),
            r.Hora
        });

        return Ok(reservasFormateadas);
    }

    // Creo una reserva
    [HttpPost]
    public ActionResult<Reserva> CrearReserva([FromBody] Reserva reserva)
    {
        if (reservas.Any(r => r.Fecha == reserva.Fecha && r.Hora == reserva.Hora))
        {
            return BadRequest(new { message = "Ya existe una reserva en ese día y horario" });
        }

        if (reservas.Any(r => r.Fecha == reserva.Fecha && r.Cliente == reserva.Cliente))
        {
            return BadRequest(new { message = "El cliente ya tiene una reserva ese día" });
        }

        reserva.Id = nextId++;
        reserva.Fecha = DateTime.ParseExact(reserva.Fecha.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null);
        reservas.Add(reserva);
        return CreatedAtAction(nameof(GetReservas), new { id = reserva.Id }, reserva);
    }
}
