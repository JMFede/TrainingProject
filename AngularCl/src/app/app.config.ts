import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import {provideNativeDateAdapter} from '@angular/material/core';
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { ProductionService } from './services/production.service';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), 
              provideAnimationsAsync(), 
              provideNativeDateAdapter(), 
              provideHttpClient(), 
              DatePipe,
            ]
};
