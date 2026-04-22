using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contract
{
    [Key]
    public int ContractId { get; set; }
    public DateTime? StartDate { get; set; } 
    public DateTime? EndDate { get; set; }
    public string? Status { get; set; } 
    public string? ServiceLevel { get; set; } 
    public string? SignedAgreementPath { get; set; } // for PDF file handling

    public int ClientId { get; set; } // FK property
    [ForeignKey("ClientId")] 
    public Client? Client { get; set; }

    public ICollection<ServiceRequest>? ServiceRequests { get; set; }
}