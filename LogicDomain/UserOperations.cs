using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProAgro.Abstractions;
using ProAgro.DAL;
using ProAgro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgro.LogicDomain
{
    public class UserOperations : IUsers
    {
        IRepository<DatabaseContext> Repository;
        IDbContextFactory<DatabaseContext> DbContextFactory;
        public UserOperations(IRepository<DatabaseContext> Repo, IDbContextFactory<DatabaseContext> Factory)
        {
            Repository = Repo;
            DbContextFactory = Factory;
        }
        public async Task<T> Login<T>(string Username, string Password) where T : class
        {
            SqlParameter User = new SqlParameter("Username", Username);    
            SqlParameter Pass = new SqlParameter("Pass", Password);

            try
            {
                var response = await Repository.ListEntityFromQuery<Usuario>("exec pgo.LoginByCredentials @Username, @Pass",
                    new object[] { User, Pass }, DbContextFactory);
                if (response != null)
                {
                    return response as T;
                }
                return new Usuario() as T;
            }
            catch (Exception ex)
            {
                return new Usuario() as T;
            }
         
        }

        public async Task<T> ChangeName<T>(string NewUsername, int UserId) where T : class
        {
            SqlParameter NewName = new SqlParameter("Username", NewUsername);
            SqlParameter UserIdCheck = new SqlParameter("UserId", UserId);
            try
            {
                var response = await Repository.ListEntityFromQuery<Usuario>("pgo.ChangeNameByUserId @Username, @UserId",
                    new object[] { NewName, UserIdCheck }, DbContextFactory);
                if (response != null)
                {
                    return response as T;
                }
                return new Usuario() as T;
            }
            catch (Exception ex)
            {
                return new Usuario() as T;
            }
        }

    }
}
