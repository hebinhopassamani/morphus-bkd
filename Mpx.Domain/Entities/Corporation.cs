namespace Mpx.Domain.Entities;

public class Corporation
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Key { get; set; }
  public required string Email { get; set; }
  public bool System { get; set; }
  public string? FullName { get; set; }
  public string? Description { get; set; }
  public Guid? CorporationId { get; set; }

  public virtual Corporation? ParentCorporation { get; set; }

  public virtual ICollection<User> Users { get; set; } = [];
  public virtual ICollection<UserStatus> UserStatus { get; set; } = [];
  public virtual ICollection<Module> Modules { get; set; } = [];
  public virtual ICollection<SubModule> SubModules { get; set; } = [];
  public virtual ICollection<Menu> Menus { get; set; } = [];
  public virtual ICollection<Corporation> Corporations { get; set; } = [];
}
