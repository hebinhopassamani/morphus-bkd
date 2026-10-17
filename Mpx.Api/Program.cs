using Mpx.Api;
using Mpx.Application;
using Mpx.Domain;
using Mpx.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

ApiDI.AddJsonSettings(builder.Services);
ApiDI.AddDependencyInjection(builder.Services);
ApiDI.AddAuth0Authentication(builder.Services, builder.Configuration);
InfrastructureDI.AddMorhusDbContext(builder.Services, builder.Configuration);
InfrastructureDI.AddDependencyInjection(builder.Services);
DomainDI.AddDependencyInjection(builder.Services);
ApplicationDI.AddDependencyInjection(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowCredentials");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
