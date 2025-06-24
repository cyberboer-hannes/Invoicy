using Invoicy.Application.Customers.Models;
using Invoicy.Application.Services.Customers.Interfaces;
using Invoicy.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Invoicy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerRequestDto request)
    {
        var customerModel = MapToCustomerModel(request);
        try
        {
            await _customerService.AddCustomerAsync(customerModel);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("telephone number"))
                return Conflict(ex.Message);  // HTTP 409 Conflict

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerRequestDto request)
    {
        if (id != request.Id)
            return BadRequest("ID mismatch");

        var customerModel = MapToCustomerModel(request);
        try
        {
            await _customerService.UpdateCustomerAsync(customerModel);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("telephone number"))
                return Conflict(ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();

        return Ok(MapToCustomerDto(customer));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        var dtos = customers.Select(MapToCustomerDto).ToList();
        return Ok(dtos);
    }

    /********** Mapping Methods **********/
    private static CustomerModel MapToCustomerModel(CustomerRequestDto dto)
    {
        return new CustomerModel
        {
            Name = dto.Name,
            TelephoneNumber = dto.TelephoneNumber,
            Address = new AddressModel
            {
                Id = 0,
                Province = dto.Address.Province,
                City = dto.Address.City,
                Suburb = dto.Address.Suburb,
                Street = dto.Address.Street,
                ZipCode = dto.Address.ZipCode
            }
        };
    }

    private static CustomerModel MapToCustomerModel(UpdateCustomerRequestDto dto)
    {
        return new CustomerModel
        {
            Id = dto.Id,
            Name = dto.Name,
            TelephoneNumber = dto.TelephoneNumber,
            Address = new AddressModel
            {
                Id = dto.Address.Id,
                Province = dto.Address.Province,
                City = dto.Address.City,
                Suburb = dto.Address.Suburb,
                Street = dto.Address.Street,
                ZipCode = dto.Address.ZipCode
            }
        };
    }

    private static CustomerResponseDto MapToCustomerDto(CustomerModel model)
    {
        return new CustomerResponseDto
        {
            Id = model.Id,
            Name = model.Name,
            TelephoneNumber = model.TelephoneNumber,
            Address = new AddressResponseDto
            {
                Id = model.Address.Id,
                Province = model.Address.Province,
                City = model.Address.City,
                Suburb = model.Address.Suburb,
                Street = model.Address.Street,
                ZipCode = model.Address.ZipCode
            }
        };
    }
}
