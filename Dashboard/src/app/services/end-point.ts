import { InjectionToken } from '@angular/core';

export let API_BASE_URL = new InjectionToken<string>('APIBaseUrl');

export function getBaseApiUrl(): string {
    return 'https://localhost:5001/api/';//window.location.hostname || window.location.href.split('#')[0] + 'api/';
}

/*

constructor(private http: HttpClient,
                @Inject(API_BASE_URL) private hostUrl) {
    }
*/