using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProAgro.Abstractions;
using ProAgro.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgro.LogicDomain
{
    public class GeorreferencesOperations : IGeorreferences
    {


        IRepository<DatabaseContext> Repository;
        IDbContextFactory<DatabaseContext> Context;
        public GeorreferencesOperations(IRepository<DatabaseContext> repo, IDbContextFactory<DatabaseContext> con )
        {
            Repository = repo;
            Context = con;
        }
        public async Task<List<T>> GetGeorreferencesByUserId<T>(int UserId) where T : class
        {
            SqlParameter User = new SqlParameter("@UserId", UserId);
            try
            {
                var georreferences = await Repository.ListEntityFromQuery<Georreferencias>("exec pgo.GetGeorreferencesByUserId @UserId", new object[] { User }, Context);
                if (georreferences != null)
                {
                    if(georreferences.Count>0)
                    return georreferences as List<T>;
                    else
                    {
                        return new List<T>();
                    }
                }
                return new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<T>();
        }
    }
}
