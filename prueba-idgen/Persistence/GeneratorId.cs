using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace PruebaIdGen.Persistence;

public class GeneratorId : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry)
    {
        var nombreTabla = entry.Context.Model.FindEntityType(entry.Entity.GetType())!.GetTableName();
        var title = entry.Property("Title").CurrentValue;

        // Obtengo el departamento de la entidad, no mapeado
        var departamento = entry.Entity.GetType().GetProperty("Departamento")!.GetValue(entry.Entity);

        var aux = entry.Context.Database.SqlQuery<int>($"SELECT 1 FROM GenID WHERE name = {nombreTabla!.ToUpper()} AND departamento = {departamento}").ToList().Count;
        if (aux != 1) {
            entry.Context.Database.ExecuteSqlRaw("INSERT INTO GenID (name, value, departamento) VALUES (@p0, @p1, @p2)", nombreTabla!.ToUpper(), 0, departamento);
        }

        aux = entry.Context.Database.SqlQuery<int>($"SELECT value FROM GenID WHERE name = {nombreTabla!.ToUpper()} AND departamento = {departamento}").ToList().First();

        aux++;
        entry.Context.Database.ExecuteSqlRaw("UPDATE GenID SET value = @p0 WHERE name = @p1 AND departamento = @p2", aux, nombreTabla.ToUpper(), departamento);

        return departamento!.ToString() + aux.ToString().PadLeft(8, '0');

    }
}