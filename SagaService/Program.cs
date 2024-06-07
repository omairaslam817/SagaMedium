using MassTransit;
using MessageBrokers;
using Microsoft.EntityFrameworkCore;
using SagaService.Models;
using SagaStateMachine;
using SagaStateMachine.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddSagaStateMachine<TicketStateMachine, TicketStateData>()
     .EntityFrameworkRepository(r =>
     {
         r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or ConcurrencyMode.Optimistic if using RowVersion
         r.ExistingDbContext<AppDbContext>();
     });

    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(RabbitMQConfig.RabbitMQURL), h =>
        {
            h.Username(RabbitMQConfig.UserName);
            h.Password(RabbitMQConfig.Password);
        });
        config.ConfigureEndpoints(context);
    });
});


// Add services to the container.
builder.Services.AddDbContextPool<AppDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
