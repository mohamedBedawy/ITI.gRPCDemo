using ITI.gRPCDemo.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
// To access gRPC list from gRPCcurl command line
builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<TelementaryTrackingService>();
    if (app.Environment.IsDevelopment())
    {
        // Enable gRPC in development only
        endpoints.MapGrpcReflectionService();
    }
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
