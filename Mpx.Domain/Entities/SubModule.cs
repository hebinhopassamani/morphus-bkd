namespace Mpx.Domain.Entities
{
  public class SubModule
  {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Key { get; set; }
    public bool System { get; set; }
    public Guid ModuleId { get; set; }
    public Guid CorporationId { get; set; }
    public string? Description { get; set; }

    public virtual Module? Module { get; set; }
    public virtual Corporation? Corporation { get; set; }
  }
}

