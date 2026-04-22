using System.ComponentModel.DataAnnotations;

public class Client
{
    [Key]
    public int ClientId { get; set; } 
    public string? Name { get; set; }
    public string? ContactDetails { get; set; }
    public string? Region { get; set; }

    public ICollection<Contract>? Contracts { get; set; } // navigation property
}