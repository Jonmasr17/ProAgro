using Microsoft.EntityFrameworkCore;
using ProAgro.Abstractions;
using ProAgro.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProAgro.Repository
{
    public class Repository : IRepository<DatabaseContext>
    {
        public Repository()
        {
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public async Task<int> SaveChanges(DatabaseContext Context)
        {
            int res = 0;
            try
            {
                res = await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                foreach (var entry in e.Entries)
                {
                    // obtener valores actuales
                    var valoresActuales = entry.CurrentValues;
                    // valores de DB
                    var valoresBD = entry.GetDatabaseValues();
                    // valores originales
                    var valoresOriginales = entry.OriginalValues;
                    foreach (var property in valoresActuales.Properties)
                    {
                        //si no es llave primaria debe guardar lo que esta en este modelo los valores actuales 
                        if (!(property.Name.Contains("id")) && !(property.Name.Contains("ID")) && !(property.IsPrimaryKey()) && !(property.Name.Contains("Id")))
                            entry.Property(property.Name).IsModified = true;

                    }
                    entry.OriginalValues["Timestamp"] = valoresBD["Timestamp"];
                    //if(entry.Entity is Payments)
                    //{

                    //    foreach(var prop in valoresActuales.Properties)
                    //    {
                    //        //valoresActuales[prop] = valoresActuales[prop];
                    //        entry.Property(prop.Name).IsModified = true;
                    //    }

                    //}
                }
                await SaveChanges(Context);
            }
            catch (DbUpdateException data)
            {
                System.Diagnostics.Debug.Write(data.Message);
            }
            catch (Exception e)
            {

            }
            return res;
        }
        public async Task<int> Save(DatabaseContext Context)
        {
            int changes = 0;
                changes = await SaveChanges(Context);
            return changes;
        }
        public async Task<List<T>> ListEntityFromQuery<T>(string Query, object[] args, IDbContextFactory<DbContext> Factory) where T : class
        {
            var Cont = Factory.CreateDbContext();

            using (Cont)
            {
                if (args != null)
                    return await Cont.Set<T>().FromSqlRaw(Query, args).ToListAsync();
                else
                    return await Cont.Set<T>().FromSqlRaw($"{Query}").ToListAsync();
            }
        }

        public async Task<int> ResponseFromQuery(string Query, object[] args, IDbContextFactory<DbContext> Factory)
        {
            var Cont = Factory.CreateDbContext();

            using (Cont)
            {
                try
                {
                    if (args != null)
                    {
                        return await Cont.Database.ExecuteSqlRawAsync(Query, args);
                    }
                    else {
                        return 0; 
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<int> ExecuteRawSql(string QueryRaw, List<object> args, IDbContextFactory<DatabaseContext> factory)
        {
            var Context = factory.CreateDbContext();
            using (Context)
            {
                try
                {
                    var response = await Context.Database.ExecuteSqlRawAsync(QueryRaw, args);
                    await Save(Context);
                    return response;
                }
                catch (Exception e)
                {

                    throw;
                }

            }
        }
    }
}
