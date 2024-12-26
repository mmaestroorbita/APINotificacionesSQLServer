using Microsoft.AspNetCore.Mvc;
using Notifications.DTO;
using NotificationService.Services;

namespace NotificationsController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<NotificacionesController> _logger;

        public NotificacionesController(IReportService reportService, ILogger<NotificacionesController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> RecibirNotificacion([FromBody] EventDto evento)
        {
            try
            {
                _logger.LogInformation($"Notificación recibida: {evento.ID_EVENTO}, {evento.MATRICULA}");

                var response = await _reportService.ProcessEventAsync(evento);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(new { mensaje = response.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la notificación");
                return BadRequest(new { error = "Error al procesar la notificación" });
            }
        }
    }


}
