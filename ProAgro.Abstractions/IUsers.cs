using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProAgro.Abstractions
{
    public interface IUsers
    {
        Task<T> Login<T>(string Username, string Password) where T:class;
        Task<T> ChangeName<T>(string NewUsername, int UserId) where T:class;
    }
}
