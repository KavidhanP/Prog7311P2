using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ServiceRequest
{
    [Key]
    public int ServiceRequestId { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public decimal CostUSD { get; set; }
    public string? Status { get; set; }

    public int ContractId { get; set; } // FK property
    [ForeignKey("ContractId")]
    public Contract? Contract { get; set; }
}