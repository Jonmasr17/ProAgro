using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProAgro.Abstractions;
using ProAgro.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace ProAgro.APIGeorreferencias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IUsers UserOperations;
        public UserController(IUsers UserOp)
        {
            UserOperations = UserOp;
        }
        [HttpPost(Name = "LoginUser")]
        public async Task<Usuario> Login([FromBody] JsonElement Params)
        {
            try
            {
                var User = System.Text.Json.JsonSerializer.Deserialize<User>(Params.ToString());

                var ResponseGeorreferences = await UserOperations.Login<List<Usuario>>(User.Username, User.Password);
                if (ResponseGeorreferences != null)
                {
                    if(ResponseGeorreferences.Count > 0)
                    {
                        return ResponseGeorreferences.FirstOrDefault();
                    }
                }
                return new Usuario();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Usuario();
            }
        }
        [HttpPost("/ChangeName")]
        public async Task<Usuario> ChangeName([FromBody] JsonElement Params)
        {
            try
            {
                var User = System.Text.Json.JsonSerializer.Deserialize<Usuario>(Params.ToString());

                var ResponseGeorreferences = await UserOperations.ChangeName<List<Usuario>>(User.nombre, Convert.ToInt32(User.idUsuario));
                if (ResponseGeorreferences != null)
                {
                    if (ResponseGeorreferences.Count > 0)
                    {
                        return ResponseGeorreferences.FirstOrDefault();
                    }
                }
                return new Usuario();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Usuario();
            }
        }
    }
}
