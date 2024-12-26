using Notifications.DTO;
using System.Data.SqlClient;

namespace NotificationService.Services
{
    public class ReportService : IReportService
    {
        private readonly string _connectionString;
        private readonly ILogger<ReportService> _logger;

        public ReportService(IConfiguration configuration, ILogger<ReportService> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> ProcessEventAsync(EventDto evento)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(@"Data Source=localhost;Initial Catalog=prueba;User ID=sa;Password=NuevaContraseñaSegura123!;MultipleActiveResultSets=True;"))
                {
                    string insertQuery = @"
                    INSERT INTO REPORTS (
                        ID_EVENTO,
                        MATRICULA,
                        ID_NAVE
                        
                    ) VALUES (
                        @IdEvento,
                        @Matricula,
                        @IdNave
                    )";

                    var parameters = new[]
                    {
                    new SqlParameter("@IdEvento", evento.ID_EVENTO),
                    new SqlParameter("@Matricula", evento.MATRICULA),
                    new SqlParameter("@IdNave", evento.ID_NAVE),
                };

                    await conexion.OpenAsync();
                    using var cmd = new SqlCommand(insertQuery, conexion);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();

                    return new ServiceResponse<bool>
                    {
                        Data = true,
                        Success = true,
                        Message = "Evento procesado correctamente"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar el evento");
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Error al procesar el evento",
                    Error = ex.Message
                };
            }
        }
    }
}