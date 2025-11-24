import { InjectionToken} from '@angular/core';
import { App } from '../app';

export interface AppSettings {
    apiUrl: string;
}

export const APP_SETTINGS = new InjectionToken<AppSettings>('APP_SETTINGS');

export const APP_SETTINGS_VALUE: AppSettings = {
    apiUrl : 'http://localhost:5000/api/',
};