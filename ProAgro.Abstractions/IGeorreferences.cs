using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProAgro.Abstractions
{
     public interface IGeorreferences
    {
        Task<List<T>> GetGeorreferencesByUserId<T>(int UserId) where T : class; 
    }
}
