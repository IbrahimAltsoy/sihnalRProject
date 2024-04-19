using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using sihnalRProject.Hubs;

var builder = WebApplication.CreateBuilder(args);

// SignalR ve CORS hizmetlerini ekleyin
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)
));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/myhub");
});

app.Run();
