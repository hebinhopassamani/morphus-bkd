using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mpx.Domain.Interfaces;
using Mpx.Application.Handlers; // Onde mora o seu EmailHandler fluente

namespace Mpx.Api.Controllers;

//[Authorize] // 🔒 Protege o endpoint exigindo o Token do Auth0 que configuramos no Angular
[ApiController]
[Route("api/v1/[controller]")] // Vira automaticamente api/v1/modules em snake_case
public class ModuleController : ControllerBase
{
  private readonly EmailHandler _emailHandler;
  private readonly IUnitOfWork _unitOfWork;

  // Construtor com injeção de dependências clássica e segura
  public ModuleController(EmailHandler emailHandler, IUnitOfWork unitOfWork)
  {
    _emailHandler = emailHandler;
    _unitOfWork = unitOfWork;
  }

  [HttpGet] // 🚀 O Angular bate aqui logo no carregamento da página!
  public IActionResult GetAllModules()
  {
    // Retorna um array vazio 200 OK para o front-end saber que a rota existe
    return Ok(new List<object>());
  }

  [HttpPost("verify")] // Bate exatamente com a rota post do seu ModuleService no front!
  public async Task<IActionResult> VerifySystemModules()
  {
    try
    {
      // 🧙‍♂️ DISPARO FLUENTE DO SEU TECLADO NOVO:
      // O Handler vai carregar o HTML da pasta física/DLL, preencher a variável
      // e salvar o registro na fila de Outbox do PostgreSQL de forma atômica!
      var emailRegistrado = await _emailHandler
          .NewEmail()
          .Subject("Auditoria de Módulos - Morphus Workspace")
          .LoadHtmlFile("welcome") // Carrega o welcome.html da pasta EmailTemplates
          .SetVariable("{{Nome}}", "Antônio Heber")
          .SetVariable("{{Empresa}}", "Morphus Enterprise Suite")
          .MailTo("hebinhopassamani@gmail.com", "Antônio Heber")
          .SendEmailAsync();

      // Retorna um JSON de sucesso que o seu front-end Angular vai ler no 'alert'
      return Ok(new { message = "Auditoria disparada com sucesso! O motor de e-mails processou a fila no PostgreSQL." });
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = $"Falha ao executar auditoria: {ex.Message}" });
    }
  }
}
