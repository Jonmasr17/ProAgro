using Entities;
using ProAgro.Entities;

namespace ProAgro.WebApp.Models
{
    public class UserFullModel
    {
        public Usuario Usuario { get; set; }
        public List<Georreferencias> Georreferencias { get; set; }
        public State State { get; set; }
    }
}
