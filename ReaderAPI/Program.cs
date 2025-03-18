using ReaderAPI.Services;

var builder = WebApplication.CreateBuilder(args);

string policyName = "PoliceCorse";

builder.Services.AddCors(options => options.AddPolicy(policyName,
    (o) =>
    {
        o.AllowAnyOrigin();
        o.AllowAnyHeader();
        o.AllowAnyMethod();
    }));

builder.Services.AddSingleton(new MessageService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyName);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();