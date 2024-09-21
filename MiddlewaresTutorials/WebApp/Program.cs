using WebApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders(); // Удаляет все провайдеры логирования
builder.Logging.AddConsole(); // Добавляет консольный логер
builder.Logging.AddDebug(); // Добавляет логер для отладки


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    app.Logger.LogInformation($"Ебашу по этому пути: {context.Request.Path}");
    await next.Invoke(); // продолжает обработку следующего middleware
});


app.UseMiddleware<RequestMidddleware>();
app.UseHttpsRedirection();


//Со сваггером такой прикол, что он прервывает запрос по конвейеру
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
