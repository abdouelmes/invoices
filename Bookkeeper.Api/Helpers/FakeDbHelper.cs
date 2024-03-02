using Bookkeeper.Api.Models;

namespace Bookkeeper.Api.Helpers;

public interface IFakeDbHelper
{
    List<Invoice> GetAllInvoices();
    
    Invoice GetInvoice(int invoiceNumber);
    List<Invoice> CreateEntry(string name, decimal amount, decimal payment);
    List<Invoice>  UpdateEntry(int id,string? name, decimal? amount, decimal? payment);

    List<string> GenerateReport();
}


public class DbHelper : IFakeDbHelper
{
    private readonly List<Invoice> _invoices = new List<Invoice>
    {
        new Invoice { InvoiceNumber = 0 , CustomerName = "customer-one", Amount = 100, Payments = new Dictionary<int, decimal>()}
    };
    
    public List<Invoice> GetAllInvoices()
    {
        return _invoices;
    }
    
    public Invoice GetInvoice(int invoiceNumber)
    {
        return _invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber );
    }
    private int lastInvoiceId = 0;  // Field to store the last used Id

    public List<Invoice> CreateEntry(string name, decimal amount, decimal payment)
    {
        
        int newId = lastInvoiceId + 1;  // Generate new Id
        Invoice invoice = new Invoice( newId,  name,  amount, payment);
        lastInvoiceId = newId;  // Update last used Id
        _invoices.Add(invoice);
        return _invoices;
    }
    
    
    public List<Invoice> UpdateEntry(int id, string? name, decimal? amount, decimal? payment)
    {
        // Find the invoice with the provided id
        Invoice invoiceToUpdate = _invoices.FirstOrDefault(i => i.InvoiceNumber == id);

        // Check if the invoice exists
        if (invoiceToUpdate != null)
        {
            // Update properties if new values are provided
            if (name != null)
            {
                invoiceToUpdate.CustomerName = name;
            }

            if (amount != null)
            {
                invoiceToUpdate.Amount = amount.Value; // Convert nullable decimal to decimal
            }

            if (payment != null)
            {
                invoiceToUpdate.Payments.Add(invoiceToUpdate.Payments.Count, payment.Value); // Convert nullable decimal to decimal
            }
        }
        else
        {
            // Handle case where invoice with given id is not found
            // You might throw an exception, log an error, or take other appropriate action
            throw new Exception($"Invoice with id {id} not found.");
        }

        return _invoices;
    }

    public  List<string> GenerateReport()
    {
        var groupedInvoices = _invoices
            .GroupBy(invoice => invoice.CustomerName)
            .Select(group => new
            {
                CustomerName = group.Key,
                TotalPayments = group.Sum(invoice => invoice.Payments?.Sum(payment => payment.Value)),
                TotalAmount = group.Sum(invoice => invoice.Amount)
            })
            .ToList();
        
        List<string> customerPayments = new List<string>();

        foreach (var item in groupedInvoices)
        {
            string customerPayment = $"{item.CustomerName}: {item.TotalAmount- item.TotalPayments :C}";
            customerPayments.Add(customerPayment);
        }

        string[] customerPaymentsArray = customerPayments.ToArray();
        return customerPayments;
    }
}