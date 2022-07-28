using API.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowCredentials()
          .AllowAnyHeader().
          AllowAnyMethod()
          .SetIsOriginAllowed(x => true)));
builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors();
app.MapControllers();
app.MapHub<MessageHub>("/messagehub");

app.Run();
