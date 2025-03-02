using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using prueba_idgen.Controllers;
using PruebaIdGen.Models;
using PruebaIdGen.Persistence;

namespace prueba_idgen_test;

public class IdGenUnitTest : IDisposable
{

    ApplicationDbContext context;

    // Setup
    public IdGenUnitTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Filename=:memory:") // SQLite en memoria
            .Options;

        context = new ApplicationDbContext(options);

        context.Database.OpenConnection();
        context.Database.EnsureCreated();

    }

    [Fact]
    public void ControllerTest()
    {
        var _mockLogger = new Mock<ILogger<HomeController>>();
        var controller = new HomeController(_mockLogger.Object, context);

        var result = controller.Registrar();

        var todo = context.Todos.First();

        Assert.NotNull(result);
        Assert.Equal("Hello World", todo.Title);
        Assert.False(todo.IsCompleted);
        Assert.Equal(11, todo.Departamento);
        Assert.Equal("1100000001", todo.Id);

    }

    [Fact]
    public void AddTodoTest()
    {

        var todo = new Todo { Title = "Hello World", IsCompleted = false, Departamento = 11 };

        context.Todos.Add(todo);
        context.SaveChanges();

        Assert.Equal(1, context.Todos.Count());
        Assert.Equal("1100000001", todo.Id);

    }

    [Fact]
    public void GeneratorIdTest() {

        var entity = new Todo { Title = "Hello World", IsCompleted = false, Departamento = 11 };
        context.Add(entity);
        context.SaveChanges();

        var entry = context.Entry(entity); // Obtiene un EntityEntry real

        var gen = new GeneratorId();
        var config = gen.GeneratesTemporaryValues;
        var value = gen.Next(entry);

        Assert.Equal("1100000002", value);
        Assert.False(config);
    }

    [Fact]
    public void ModelTest() {
        
        var genId = new GenID {
            Name = "TODOS",
            Value = 1,
            Departamento = 11
        };

        Assert.Equal("TODOS", genId.Name);


    }

    // teardown
    public void Dispose()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

}
