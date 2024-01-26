namespace CleanArchitecture.Domain.Common;

public abstract class BaseDomainModel
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set;}
}
