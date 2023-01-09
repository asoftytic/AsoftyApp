using AsoftyBackend.Infrastructure.Data.Attributes;

namespace AsoftyBackend.Infrastructure.Data.Model;

public class User : DbEntity
{
    [PrimaryKey]
    public int UserId { get; set; }
    public int CompanyCode { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int? Status { get; set; } 
    public bool? Enabled { get; set; }
    public int? RollCode { get; set; }
}