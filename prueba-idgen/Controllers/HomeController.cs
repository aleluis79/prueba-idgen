using Microsoft.AspNetCore.Mvc;
using PruebaIdGen.Models;
using PruebaIdGen.Persistence;

namespace prueba_idgen.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("registrar")]
    public ActionResult<string> Registrar()
    {
        Todo todo = new Todo
        {
            Title = "Hello World",
            IsCompleted = false,
            Departamento = 11
        };

        _context.Todos.Add(todo);
        _context.SaveChanges();
        
        return Ok("pong");
    }
}
