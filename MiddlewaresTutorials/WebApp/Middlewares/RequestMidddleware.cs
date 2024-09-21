namespace WebApp.Middlewares
{
    public class RequestMidddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestMidddleware> _logger;
        public RequestMidddleware(RequestDelegate next, ILogger<RequestMidddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {

            // По умолчанию статус код ответа - 200, даже если еще не сформирован ответ
            var start = DateTime.Now;
            _logger.LogInformation($"Начало обработки запроса: {context.Request.Path} в {start}");

            await _next(context);

            var end = DateTime.Now;
            _logger.LogInformation($"Конец обработки запроса: {context.Request.Path} в {end}");
            _logger.LogInformation($"Время выполнения запроса: {end - start}");
        }
    }
}
