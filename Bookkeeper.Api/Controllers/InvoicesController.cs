using Bookkeeper.Api.Helpers;
using Bookkeeper.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeper.Api.Controllers;

[ApiController]
[Route("/api/")]
public class InvoicesController : ControllerBase
{
    private IFakeDbHelper _repo;

    public InvoicesController(IFakeDbHelper repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Route("/invoices")] 
    public List<Invoice> Invoices()
    {
        return _repo.GetAllInvoices();
    }
    
    [HttpGet]
    [Route("/invoice")] 
    public Invoice Invoice(int id)
    {
        return _repo.GetInvoice(id);
    }

    [HttpPost]
    [Route("/invoice")]
    public List<Invoice> CreateInvoice( string name, decimal amount, int payment)
    {
        return _repo.CreateEntry(name, amount, payment);
    }
    
    [HttpPost]
    [Route("/invoice/update")]
    public List<Invoice> UpdateInvoice(int id, string name, decimal amount, int payment)
    {
        return _repo.UpdateEntry( id,  name,  amount,  payment);
        
    }
    [HttpPost]
    [Route("/invoice/reports")]
    public   List<string> Report()
    {
        return _repo.GenerateReport();
        
    }
}
