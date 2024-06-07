using MassTransit;
using MessageBrokers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TicketService.Api.Consumers;
using TicketService.Api.Data;
using TicketService.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextPool<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITicketServices,TicketServices>();

builder.Services.AddMassTransit(x =>
{
    var entryAssembly = Assembly.GetExecutingAssembly();
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<GetValueConsumer>();
    x.AddConsumer<GenerateTicketCancelConsumer>(); 
   // x.AddConsumers(entryAssembly); //We registered the consumer by loading executing assembly and giving it to the AddConsumers() method.

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

//builder.Services.AddMassTransit(cfg =>
//{
//    cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
//    {
//        cfg.ReceiveEndpoint(MessageBrokers.RabbitMQQueues.SagaBusQueue, ep =>
//        {
//            ep.PrefetchCount = 10;
//            // Get Consumer
//            ep.ConfigureConsumer<GetValueConsumer>(provider);
//            // Cancel Consumer
//            ep.ConfigureConsumer<GenerateTicketCancelConsumer>(provider);
//        });
//    }));

//    cfg.AddConsumer<GetValueConsumer>();
//    cfg.AddConsumer<GenerateTicketCancelConsumer>();
//});

builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromSeconds(30);
    options.StopTimeout = TimeSpan.FromMinutes(1);

});
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
