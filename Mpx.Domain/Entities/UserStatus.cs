namespace Mpx.Domain.Entities
{
  public class UserStatus
  {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Key { get; set; }
    public bool System { get; set; }
    public Guid CorporationId { get; set; }
    public string? Description { get; set; }

    public virtual Corporation? Corporation { get; set; }
    public virtual ICollection<User> Users { get; set; } = [];
  }
}

