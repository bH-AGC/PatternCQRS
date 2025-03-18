using InternalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

string policyName = "WriterAPIPolicy";

builder.Services.AddCors(options => options.AddPolicy(policyName, policy =>
{
    policy.WithOrigins("http://localhost:7065")
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

builder.Services.AddSingleton(new UserService(builder.Configuration.GetConnectionString("DefaultConnection")));

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