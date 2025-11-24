import { Injectable } from "@angular/core";
import { WeatherService } from "./weather.service";
import { Observable, catchError, map, shareReplay, throwError } from "rxjs";
import { ForecastModel } from "../models/forecastmodel";
import { LocationModel } from "../models/locationmodel";

@Injectable({ 
    providedIn: 'root' 
})
export class WeatherApiService {
    private locationsCache$: Observable<LocationModel[]> | null = null;

    constructor(
        private weatherHttp: WeatherService
    ){}

    getLocations(): Observable<LocationModel[]> {
        if (this.locationsCache$) return this.locationsCache$;
        this.locationsCache$ = this.weatherHttp.getLocations();
        return this.locationsCache$;
    }

    getForecast(globalIdLocal: number): Observable<ForecastModel[]> {
        const now = Date.now();
        const obs$ = this.weatherHttp.getForecast(globalIdLocal).pipe(
            map(r => this.transformForecast(r)),
            shareReplay(1),
            catchError(err => this.handleError(err))
        );
        return obs$;
    }

    private transformForecast(raw: any): ForecastModel[] {
        // normalize numbers and dates here
        return raw as ForecastModel[];
    }

    private handleError(err: any): Observable<never> {
        // convert to friendly message
        return throwError(() => new Error('API error'));
    }
}