using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class Production : BaseEntity
{
    public Production()
    {
        SetCreatedAt();
        SetUpdatedAt();
    }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Code { get; private set; }
    
    [Required]
    public string Order { get; private set; }

    [Required]
    [MinLength(3)]
    [MaxLength(300)]
    public string Customer { get; private set; }
    public StatusProduction Status { get; private set; }

    public static Production CreateProduction()
    {
        var result = new Production();
        result.Status = StatusProduction.InProgress;
        return result;
    }
    
    public Production SetOrder(string order)
    {
        if(order == null)
            throw new Exception("The order number cannot be null");
        
        Order = order;
        return this;
    }

    public Production SetCustomer(string customer)
    {
        if(customer.Length < 3)
            throw new Exception("Customer name cannot be less than 3 characters");
        
        if(customer.Length > 300)
            throw new Exception("Customer name cannot be more than 300 characters");
        
        Customer = customer;
        return this;
    }

    public Production NextStatus()
    {
        if (Status == StatusProduction.Cancel)
            throw new Exception("Production has already been canceled");
        
        switch (Status)
        {
            case StatusProduction.Received:
                Status = StatusProduction.InProgress;
                break;
            case StatusProduction.InProgress:
                Status = StatusProduction.Ready;
                break;
            case StatusProduction.Ready:
                Status = StatusProduction.Finished;
                break;
        }
        return this;
    }

    public Production CancelProduction()
    {
        if(Status == StatusProduction.Finished)
            throw new Exception("Production has already been finished");
        
        Status = StatusProduction.Cancel;
        return this;
    }
}