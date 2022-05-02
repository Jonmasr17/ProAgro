namespace ProAgro.WebApp.Shared
{
    public partial class MainLayout
    {
        protected override async Task OnAfterRenderAsync(bool firstrender)
        {
            if(firstrender)
            Nav.NavigateTo("/Login");
        }
    }
}
