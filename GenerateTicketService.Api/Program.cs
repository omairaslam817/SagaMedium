using GenerateTicketService.Api.Common.mapping;
using GenerateTicketService.Api.Consumer;
using GenerateTicketService.Api.Data;
using GenerateTicketService.Api.Services;
using MassTransit;
using MessageBrokers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextPool<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITicketInfoService,TicketInfoService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<GenerateTicketConsumer>();
    x.AddConsumer<CancelSendingEmailConsumer>();

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
