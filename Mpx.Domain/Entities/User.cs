namespace Mpx.Domain.Entities;

public class User
{
  public Guid Id { get; set; }
  public required string? Name { get; set; }
  public required string? Email { get; set; }
  public bool EmailConfirmed { get; set; }
  public bool PhoneConfirmed { get; set; }
  public bool TermsConfirmed { get; set; }
  public bool System { get; set; }
  public Guid UserStatusId { get; set; }
  public Guid CorporationId { get; set; }
  public string? PasswordHash { get; set; }
  public string? NickName { get; set; }
  public string? Picture { get; set; }
  public string? PhoneNumber { get; set; }

  public virtual Corporation? Corporation { get; set; }
  public virtual UserStatus? UserStatus { get; set; }
}

