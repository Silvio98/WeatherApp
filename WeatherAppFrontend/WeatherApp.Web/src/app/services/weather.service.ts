import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { APP_SETTINGS, AppSettings }  from "../config/app-settings";
import { LocationModel } from "../models/locationmodel";
import { ForecastModel } from "../models/forecastmodel";

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
    constructor(
        private http: HttpClient,
        @Inject(APP_SETTINGS) private appSettings: AppSettings
    ) {}
    getForecast(globalIdLocal: number) {
        // Build URL dynamically using your settings
        const url = `${this.appSettings.apiUrl}/weather/${globalIdLocal}`;
        // Use HttpClient normally
        return this.http.get<ForecastModel[]>(url)
    }

    getLocations(){
        const url = `${this.appSettings.apiUrl}/locations/`;
        return this.http.get<LocationModel[]>(url);
    }
}