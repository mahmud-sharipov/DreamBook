import { InjectionToken } from '@angular/core';

export let API_BASE_URL = new InjectionToken<string>('APIBaseUrl');

export function getBaseApiUrl(): string {
    return 'http://localhost:5000/api/';
}