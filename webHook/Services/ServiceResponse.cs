namespace NotificationService.Services
{
    public class ServiceResponse<T>
    {
        public bool Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}