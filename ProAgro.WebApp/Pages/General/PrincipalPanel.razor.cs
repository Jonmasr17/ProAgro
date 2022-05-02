using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using ProAgro.Entities;
using ProAgro.WebApp.Models;

namespace ProAgro.WebApp.Pages.General
{
    public partial class PrincipalPanel
    {
        public HttpClient HttpClient = new HttpClient();
        public string ActualName { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                var longi = UserFullmodel.Georreferencias.Where(o => o.longitud != null).Select(o => o.longitud).ToList();
                var lats = UserFullmodel.Georreferencias.Where(o=>o.latitud!=null).Select(o=>o.latitud).ToList();
                await JSRuntime.InvokeVoidAsync("initialize", lats, longi);
            }
        }
        public async Task<string> OnChangeName()
        {
            if (!string.IsNullOrEmpty(ActualName)){
                var stream = new MemoryStream();
                var user = new Usuario() { nombre = ActualName, idUsuario = UserFullmodel.Usuario.idUsuario };
                var dataJson = JsonConvert.SerializeObject(user);
                var ResponseLogin = await HttpClient.PostAsJsonAsync($"https://localhost:5001/ChangeName/", dataJson);
                return UserFullmodel.Usuario.nombre;
            }
            else
            {
                return UserFullmodel.Usuario.nombre;

            }
            StateHasChanged();
        }
    }
}
