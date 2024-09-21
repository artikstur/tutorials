using WebApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders(); // ������� ��� ���������� �����������
builder.Logging.AddConsole(); // ��������� ���������� �����
builder.Logging.AddDebug(); // ��������� ����� ��� �������


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    app.Logger.LogInformation($"����� �� ����� ����: {context.Request.Path}");
    await next.Invoke(); // ���������� ��������� ���������� middleware
});


app.UseMiddleware<RequestMidddleware>();
app.UseHttpsRedirection();


//�� ��������� ����� ������, ��� �� ���������� ������ �� ���������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
