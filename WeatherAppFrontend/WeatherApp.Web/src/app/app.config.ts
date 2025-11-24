import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideHttpClient } from '@angular/common/http';
import { APP_SETTINGS, APP_SETTINGS_VALUE } from './config/app-settings';
import { APP_SETTINGS_PROD } from './config/app-settings.prod';

const isProd = import.meta.env.PROD;

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    // This provides the configuration object to the entire Angular app.
    { 
      // If production → use production settings
      // If development → use default settings
      provide: APP_SETTINGS,
      useValue: isProd ? APP_SETTINGS_PROD : APP_SETTINGS_VALUE 
    }
  ]
};
