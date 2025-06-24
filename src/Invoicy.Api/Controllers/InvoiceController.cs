using Invoicy.Api.DTOs;
using Invoicy.Application.Invoices.Commands;
using Invoicy.Application.Invoices.Models;
using Invoicy.Application.Services.Invoices.Interfaces;
using Invoicy.Application.Customers.Models;
using Microsoft.AspNetCore.Mvc;
using Invoicy.Application.Invoices.Queries;

namespace Invoicy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice(InvoiceHeaderRequestDto dto)
    {
        var command = MapToCreateInvoiceCommand(dto);
        try
        {
            await _invoiceService.CreateInvoiceAsync(command);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("telephone number"))
                return Conflict(ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInvoices()
    {
        var invoices = await _invoiceService.GetAllInvoicesAsync();
        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoice(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
        if (invoice == null)
            return NotFound();

        var responseDto = new InvoiceHeaderResponseDto
        {
            InvoiceNumber = invoice.InvoiceNumber,
            InvoiceDate = invoice.InvoiceDate,
            Customer = new CustomerResponseDto
            {
                Id = invoice.Customer.Id,
                Name = invoice.Customer.Name,
                TelephoneNumber = invoice.Customer.TelephoneNumber,
                Address = new AddressResponseDto
                {
                    Id = invoice.Customer.Address.Id,
                    Province = invoice.Customer.Address.Province,
                    City = invoice.Customer.Address.City,
                    Suburb = invoice.Customer.Address.Suburb,
                    Street = invoice.Customer.Address.Street,
                    ZipCode = invoice.Customer.Address.ZipCode
                }
            },
            InvoiceDetails = invoice.InvoiceDetails.Select(d => new InvoiceDetailResponseDto
            {
                ItemDescription = d.ItemDescription,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToList()
        };

        return Ok(responseDto);
    }

    // ********** Mapping Methods **********

    private static CreateInvoiceCommand MapToCreateInvoiceCommand(InvoiceHeaderRequestDto dto)
    {
        return new CreateInvoiceCommand
        {
            Customer = new CustomerModel
            {
                Name = dto.Customer.Name,
                TelephoneNumber = dto.Customer.TelephoneNumber,
                Address = new AddressModel
                {
                    Province = dto.Customer.Address.Province,
                    City = dto.Customer.Address.City,
                    Suburb = dto.Customer.Address.Suburb,
                    Street = dto.Customer.Address.Street,
                    ZipCode = dto.Customer.Address.ZipCode
                }
            },
            InvoiceDetails = dto.InvoiceDetails.Select(d => new InvoiceDetailModel
            {
                ItemDescription = d.ItemDescription,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToList()
        };
    }

    //private static InvoiceHeaderResponseDto MapToInvoiceHeaderResponseDto(GetInvoiceResult result)
    //{
    //    return new InvoiceHeaderResponseDto
    //    {
    //        InvoiceNumber = result.InvoiceNumber,
    //        InvoiceDate = result.InvoiceDate,
    //        Customer = new CustomerResponseDto
    //        {
    //            Id = result.Customer.Id,
    //            Name = result.Customer.Name,
    //            TelephoneNumber = result.Customer.TelephoneNumber,
    //            Address = new AddressResponseDto
    //            {
    //                Id = result.Customer.Address.Id,
    //                Province = result.Customer.Address.Province,
    //                City = result.Customer.Address.City,
    //                Suburb = result.Customer.Address.Suburb,
    //                Street = result.Customer.Address.Street,
    //                ZipCode = result.Customer.Address.ZipCode
    //            }
    //        },
    //        InvoiceDetails = result.InvoiceDetails.Select(d => new InvoiceDetailResponseDto
    //        {
    //            ItemDescription = d.ItemDescription,
    //            Quantity = d.Quantity,
    //            UnitPrice = d.UnitPrice
    //        }).ToList()
    //    };
    //}
}
