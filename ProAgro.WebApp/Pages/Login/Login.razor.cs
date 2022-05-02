using Microsoft.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using ProAgro.Entities;
using Newtonsoft.Json;
using Microsoft.JSInterop;
using ProAgro.WebApp.Models;
using Entities;
using System.Collections.Generic;

namespace ProAgro.WebApp.Pages.Login
{
    public partial class Login
    {
        public string Username = "";

        
        public HttpClient Client { get; set; }
        public string Password = "";
        public bool Logged = false;
        protected override async Task OnInitializedAsync()
        {
            Client = new HttpClient();
        }
        
        public async Task<bool> LoginSendData()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                var stream = new MemoryStream();
                var user = new User() { Username = Username, Password = Password };
                var dataJson = JsonConvert.SerializeObject(user);
                var ResponseLogin = await Client.PostAsJsonAsync($"https://localhost:5001/api/User/", dataJson);
                if (ResponseLogin.IsSuccessStatusCode)
                {
                    var responseReaded = await ResponseLogin.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseReaded))
                    {
                        UserFullModel.Usuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(responseReaded);
                        await JSRuntime.InvokeVoidAsync("AlertSuccess", null);
                        if (Convert.ToInt32(UserFullModel.Usuario.idUsuario) > 0)
                        {
                            var usuario = new Usuario() { idUsuario = UserFullModel.Usuario.idUsuario, nombre = "" };
                            var latlon= await Client.PostAsJsonAsync($"https://localhost:5001/api/Georreferencias/", usuario);
                            if (latlon.IsSuccessStatusCode)
                            {
                                var latlongslist = await  (latlon.Content.ReadAsStringAsync());
                                var responseGeo = System.Text.Json.JsonSerializer.Deserialize<List<Georreferencias>>(latlongslist);
                                if(responseGeo.Count > 0)
                                {
                                    UserFullModel.Georreferencias = responseGeo;    
                                }
                                NavigationManager.NavigateTo("/Panel");
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}