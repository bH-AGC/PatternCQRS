using WriterAPI.Services;

var builder = WebApplication.CreateBuilder(args);

string policyName = "PoliceCorse";

builder.Services.AddCors(options => options.AddPolicy(policyName,
    (o) =>
    {
        o.AllowAnyOrigin();
        o.AllowAnyHeader();
        o.AllowAnyMethod();
    }));


builder.Services.AddHttpClient();

builder.Services.AddSingleton<MessageService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    string messagesConnectionString = configuration.GetConnectionString("DefaultConnection");
    return new MessageService(messagesConnectionString, sp.GetRequiredService<HttpClient>());
});

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