namespace Mpx.Domain.Entities
{
  public class EmailCc
  {
    public Guid Id { get; set; }

    // O required obriga o uso do inicializador de objeto
    public required string Mail { get; set; }
    public string? Name { get; set; }

    public Guid EmailId { get; set; }
    public required virtual Email Email { get; set; }
  }
}

