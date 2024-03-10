using ApiPhotoExpress.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPhotoExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly Context dbContext;

        public EventoController(Context context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IEnumerable<EventosModel> Get()
        {
            List<EventosModel> eventos = dbContext.Evento.ToList();
            return eventos;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            EventosModel evento = dbContext.Evento.FirstOrDefault(c => c.IdEvento == id);

            if (evento == null)
            {
                return NotFound($"El evento con el ID {id} no se encontró.");
            }

            return Ok(evento);
        }


        [HttpPost]
        public IActionResult PostEvento(EventosModel evento)
        {
            try
            {
                dbContext.Evento.Add(evento);
                dbContext.SaveChanges();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el evento: {ex}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, EventosModel evento)
        {
            try
            {
                // Buscar el evento por su ID en la base de datos
                EventosModel eventoExistente = dbContext.Evento.FirstOrDefault(c => c.IdEvento == id);

                // Verificar si el evento existe
                if (eventoExistente == null)
                {
                    // Si el evento no se encuentra, devolver un código de estado 404 (Not Found)
                    return NotFound(new { mensaje = $"El evento con el ID {id} no se encontró." });
                }

                // Actualizar el evento existente con los datos recibidos
                eventoExistente.NombreInstitucion = evento.NombreInstitucion;
                eventoExistente.DireccionInstitucion = evento.DireccionInstitucion;
                eventoExistente.NumeroAlumnos = evento.NumeroAlumnos;
                eventoExistente.HoraInicio = evento.HoraInicio;
                eventoExistente.FechaEvento = evento.FechaEvento;
                eventoExistente.CostoServicio = evento.CostoServicio;
                eventoExistente.ServicioTogaBirrete = evento.ServicioTogaBirrete;

                // Guardar los cambios en la base de datos
                dbContext.SaveChanges();

                // Devolver un objeto JSON con el mensaje de éxito y los datos actualizados del evento
                return Ok(new { mensaje = $"El evento con el ID {id} fue actualizado correctamente.", evento = eventoExistente });
            }
            catch (Exception ex)
            {
                // Si ocurre algún error durante la actualización, devolver un código de estado 500 (Internal Server Error)
                return StatusCode(500, new { mensaje = $"Error al actualizar el evento: {ex.Message}" });
            }
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Buscar el evento por su ID en la base de datos
            EventosModel evento = dbContext.Evento.FirstOrDefault(c => c.IdEvento == id);

            // Verificar si el evento existe
            if (evento == null)
            {
                // Si el evento no se encuentra, devolver un código de estado 404 (Not Found)
                return NotFound(new { mensaje = $"El evento con el ID {id} no se encontró." });
            }

            try
            {
                // Eliminar el evento de la base de datos
                dbContext.Evento.Remove(evento);
                dbContext.SaveChanges();

                // Devolver un objeto JSON con el mensaje de éxito
                return Ok(new { mensaje = $"El evento con el ID {id} fue eliminado correctamente." });
            }
            catch (Exception ex)
            {
                // Si ocurre algún error durante la eliminación, devolver un código de estado 500 (Internal Server Error)
                return StatusCode(500, new { mensaje = $"Error al eliminar el evento: {ex.Message}" });
            }
        }


    }
}
