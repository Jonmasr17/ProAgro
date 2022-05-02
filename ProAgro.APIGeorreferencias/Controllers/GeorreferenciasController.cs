using Entities;
using Microsoft.AspNetCore.Mvc;
using ProAgro.Abstractions;
using System.Text.Json;

namespace ProAgro.APIGeorreferencias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeorreferenciasController : ControllerBase
    {
        IGeorreferences Georreferences;
        public GeorreferenciasController(IGeorreferences Geo)
        {
            Georreferences = Geo;

        }
        [HttpPost(Name = "GetGeorreferences")]
        public async Task<List<Georreferencias>> Get([FromBody] JsonElement UserId) {

            try
            {
                var User = System.Text.Json.JsonSerializer.Deserialize<Usuario>(UserId.ToString());
                var ResponseGeorreferences = await Georreferences.GetGeorreferencesByUserId<Georreferencias>(Convert.ToInt32(User.idUsuario));

                return ResponseGeorreferences;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Georreferencias>();
            }
        }
    }
}
