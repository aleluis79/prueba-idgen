using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaIdGen.Models;

public class Todo
{
    public string? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }

    [NotMapped]
    public int Departamento { get; set; }
}