using FluentValidation;
using Invoicy.Api.DTOs;
using Invoicy.Api.Validators;
using Invoicy.Application.Abstractions.Clock;
using Invoicy.Application.Repositories.Interfaces;
using Invoicy.Application.Services.Generators.Interfaces;
using Invoicy.Application.Services.Invoices.Interfaces;
using Invoicy.Application.Services.Invoices.Implementations;
using Invoicy.Infrastructure.Clock;
using Invoicy.Infrastructure.Persistence;
using Invoicy.Infrastructure.Repositories.Implementations;
using Invoicy.Infrastructure.Services.Generators.Implementations;
using Microsoft.EntityFrameworkCore;
using Invoicy.Api.Repositories.Interfaces;
using Invoicy.Application.Invoices.Commands;
using Invoicy.Application.Validators;
using Invoicy.Application.Services.Customers.Implementations;
using Invoicy.Application.Services.Customers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configure database 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceNumberGenerator, InvoiceNumberGenerator>();

// Register validators for Application layer
builder.Services.AddScoped<IValidator<CreateInvoiceCommand>, CreateInvoiceCommandValidator>();

// Register validators for Presentation layer DTOs
builder.Services.AddScoped<IValidator<InvoiceHeaderRequestDto>, InvoiceHeaderRequestDtoValidator>();
builder.Services.AddScoped<IValidator<CustomerResponseDto>, CustomerResponseDtoValidator>();

// Register infrastructure providers
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

// Add controllers (Web API)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("http://localhost:4200")  
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend"); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
