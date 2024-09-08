using IdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add service extensions
var serviceExtensions = new ServiceExtensions(builder.Services, builder.Configuration);
serviceExtensions.IdentityServerConfiguration();
serviceExtensions.AuthorizationConfiguration();
serviceExtensions.AuthenticationConfiguration();
serviceExtensions.DatabaseConfiguration();
serviceExtensions.DIConfiguration();
serviceExtensions.HostedServiceConfiguration();
serviceExtensions.MediatorConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseIdentityServer();
//app.UseAuthorization();
//app.UseAuthentication();

//app.MapControllers();

//app.Run();

app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
