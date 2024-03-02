namespace Bookkeeper.Api.Models;

public class Invoice
{
    public Invoice(int invoiceNumber, string customerName, decimal amount, decimal payments)
    {
        InvoiceNumber = invoiceNumber;
        CustomerName = customerName;
        Amount = amount;
        Payments = new Dictionary<int, decimal>();  
        Payments.Add(Payments.Count, payments);
    }

    public Invoice()
    {
        
    }
    public int InvoiceNumber { get; set; }
    public string? CustomerName {get;set; }
    public decimal Amount { get; set; }
    public Dictionary<int, decimal>? Payments { get; set; } 
    
    
}