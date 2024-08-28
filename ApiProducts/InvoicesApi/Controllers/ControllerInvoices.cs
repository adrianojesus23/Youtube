using InvoicesApi.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.Results;

namespace InvoicesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _invoiceService.Get("","");

        if(result.IsError())
            return NotFound(result.AsT1);

        //if (result.IsSuccess())
            
        
        return Ok(result.AsT0);
    }
    
}