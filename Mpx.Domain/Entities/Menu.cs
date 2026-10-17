namespace Mpx.Domain.Entities;

public class Menu
{
  public Guid Id { get; set; }
  public required string Label { get; set; }
  public required string Application { get; set; }
  public bool System { get; set; }
  public Guid CorporationId { get; set; }
  public string? Url { get; set; }
  public string? Icon { get; set; }
  public string? Description { get; set; }
  public Guid? MenuId { get; set; }

  public virtual Corporation? Corporation { get; set; }
  public virtual ICollection<Menu> Menus { get; set; } = [];
}

