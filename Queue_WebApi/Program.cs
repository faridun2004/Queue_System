using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Queue_WebApi.CQRS.Handlers;
using Queue_WebApi.CQRS.Queries.Queues;
using FluentValidation;
using Queue_WebApi.DataBase;
using Queue_WebApi.CQRS.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<QueueDbContext>(con=>con.UseSqlServer(builder.Configuration["ConnectionString"]));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IValidator<AddApointmentCommand>, AddApointmentCommandValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<QueueDbContext>();
        context.Database.Migrate();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
