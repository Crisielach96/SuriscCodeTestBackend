using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace SistemaReservasAPI.Controllers;

[ApiController]
[Route("api/servicios")]
public class ServiciosController : ControllerBase
{
    private static readonly List<string> servicios = new List<string>
    {
        "Cambio de aceite", "Alineación y balanceo", "Frenos", "Escaneo y diagnóstico", "Afinación de motor", "Cambio de baterías", "Cambio de neumáticos", "Mantenimiento preventivo", "Revisión del sistema eléctrico", "Reparación de transmisión"
    };

    // Obtengo lista de servicios
    [HttpGet]
    public ActionResult<IEnumerable<string>> GetServicios()
    {
        return Ok(servicios);
    }
}
