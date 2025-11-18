using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class LocationModel
    {
        public int IdRegiao { get; private set; }
        public int GlobalIdLocal { get; private set; }
        public int IdConcelho { get; private set; }
        public int IdDistrito { get; private set; }
        public String NomeDistrito { get; private set; }

        public LocationModel()
        {
        }

        public LocationModel(int IdRegiao, int GlobalIdLocal, int IdConcelho, int IdDistrito, String NomeDistrito)
        {
            this.IdRegiao = IdRegiao;
            this.GlobalIdLocal = GlobalIdLocal;
            this.IdConcelho = IdConcelho;
            this.IdDistrito = IdDistrito;
            this.NomeDistrito = NomeDistrito;
        }
    }
}
