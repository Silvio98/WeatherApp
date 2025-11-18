using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class ForecastModel
    {
        public double TempMin { get; private set; }
        public double TempMax { get; private set; }
        public String ClassWind { get; private set; }
        public String WindDirection { get; private set; }
        public double ProbPrecipitacion { get; private set; }
        public String ClassPrecipitacion { get; private set; }
        public int IdWeatherType { get; private set; }
        public Boolean Diurno { get; private set; }

        public ForecastModel()
        {
        }

        public ForecastModel(double ProbPrecipitacion, double TempMin, double TempMax, String WindDirection, int IdWeatherType, int ClassWind, int ClassPrecipitacion, int Hora)
        {
            this.TempMin = TempMin;
            this.TempMax = TempMax;

            switch (ClassWind)
            {
                case 1:
                    this.ClassWind = "Fraco";
                    break;
                case 2:
                    this.ClassWind = "Moderado";
                    break;
                case 3:
                    this.ClassWind = "Forte";
                    break;
                case 4:
                    this.ClassWind = "Muito Forte";
                    break;
                case -99:
                    this.ClassWind = "Informação Indisponível";
                    break;
            }
            this.WindDirection = WindDirection;

            this.ProbPrecipitacion = ProbPrecipitacion;
            switch (ClassPrecipitacion)
            {
                case 0:
                    this.ClassPrecipitacion = "Sem precipitação";
                    break;
                case 1:
                    this.ClassPrecipitacion = "Chuva fraca";
                    break;
                case 2:
                    this.ClassPrecipitacion = "Chuva moderada";
                    break;
                case 3:
                    this.ClassPrecipitacion = "Chuva forte";
                    break;
                case -99:
                    this.ClassPrecipitacion = "Informação Indisponível";
                    break;
            }

            this.IdWeatherType = IdWeatherType;
            if (Hora > 19 || Hora < 7)
            {
                this.Diurno = false;
            }
            else
            {
                this.Diurno = true;
            }
        }
    }
}
