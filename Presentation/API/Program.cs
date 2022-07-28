var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowCredentials()
          .AllowAnyHeader().
          AllowAnyMethod()
          .SetIsOriginAllowed(x => true)));
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors();
app.MapControllers();

app.Run();
