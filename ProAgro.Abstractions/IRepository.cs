using Microsoft.EntityFrameworkCore;
using ProAgro.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProAgro.Abstractions
{
    public interface IRepository<C> where C : DbContext
    {
        Task<List<T>> ListEntityFromQuery<T>(string Query, object[] args, IDbContextFactory<DbContext> Factory) where T : class;
        Task<int> ResponseFromQuery(string Query, object[] args, IDbContextFactory<DbContext> Factory);
    }
}
