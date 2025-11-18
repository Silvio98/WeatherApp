using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Models;
using JsonException = System.Text.Json.JsonException;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<LocationModel>> GetLocations()
        {
            try
            {
                String Url = "https://api.ipma.pt/public-data/forecast/locations.json";

                HttpResponseMessage response = await _httpClient.GetAsync(Url);
                response.EnsureSuccessStatusCode();
                String jsonResponse = await response.Content.ReadAsStringAsync();

                //Parse the response and populate LocationModel list
                // Parse with JsonDocument
                List<LocationModel> Locations = new List<LocationModel>();

                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;

                    foreach (JsonElement location in root.EnumerateArray())
                    {
                        int IdRegiao = location.GetProperty("idRegiao").GetInt32();
                        int GlobalIdLocal = location.GetProperty("globalIdLocal").GetInt32();
                        int IdConcelho = location.GetProperty("idConcelho").GetInt32();
                        int IdDistrito = location.GetProperty("idDistrito").GetInt32();
                        String NomeDistrito = location.GetProperty("local").GetString() ?? string.Empty;

                        LocationModel Location = new LocationModel(IdRegiao, GlobalIdLocal, IdConcelho, IdDistrito, NomeDistrito);
                        Locations.Add(Location);
                    }
                }

                return Locations;
            }
            catch (HttpRequestException ex)
            {
                // Handle network errors
                throw new Exception("Failed to fetch locations from API", ex);
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing errors
                throw new Exception("Failed to parse locations JSON", ex);
            }
        }

        public async Task<List<ForecastModel>> GetForecast(int GlobalIdLocal)
        {
            try
            {
                //It seems to only work for cities
                String Url = "https://api.ipma.pt/open-data/forecast/meteorology/cities/daily/" + GlobalIdLocal + ".json";
                //Implementation to fetch forecast data from IPMA API
                HttpResponseMessage response = await _httpClient.GetAsync(Url);
                response.EnsureSuccessStatusCode();
                String jsonResponse = await response.Content.ReadAsStringAsync();

                //Parse the response and populate ForecastModel list with 5 days prevision
                // Parse with JsonDocument
                List<ForecastModel> Forecasts = new List<ForecastModel>();
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;

                    // Access the "data" array
                    JsonElement dataArray = root.GetProperty("data");

                    // Iterate through each forecast
                    foreach (JsonElement forecast in dataArray.EnumerateArray())
                    {
                        double ProbPrecipitacion = double.Parse(forecast.GetProperty("precipitaProb").GetString(), CultureInfo.InvariantCulture);
                        double TempMin = double.Parse(forecast.GetProperty("tMin").GetString(), CultureInfo.InvariantCulture);
                        double TempMax = double.Parse(forecast.GetProperty("tMax").GetString(), CultureInfo.InvariantCulture);
                        String WindDirection = forecast.GetProperty("predWindDir").GetString();
                        int IdWeatherType = forecast.GetProperty("idWeatherType").GetInt32();
                        int ClassWind = forecast.GetProperty("classWindSpeed").GetInt32();
                        int ClassPrecipitacion = 0;
                        if (forecast.TryGetProperty("classPrecInt", out JsonElement classPrecIntElement))
                        {
                            ClassPrecipitacion = classPrecIntElement.GetInt32();
                        }
                        DateTime DataUpdate = root.GetProperty("dataUpdate").GetDateTime();
                        int Hour = DataUpdate.Hour;

                        ForecastModel Forecast = new ForecastModel(ProbPrecipitacion, TempMin, TempMax, WindDirection, IdWeatherType, ClassWind, ClassPrecipitacion, Hour);
                        Forecasts.Add(Forecast);
                    }
                }

                return Forecasts;

            } catch (HttpRequestException ex)
            {
                // Handle network errors
                throw new Exception("Failed to fetch forecast from API", ex);
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing errors
                throw new Exception("Failed to parse forecast JSON", ex);
            }
        }


    }
}
