import { Component, Inject, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
    selector: 'fetchdata31',
    templateUrl: './fetchdata31.component.html'
})
export class FetchData31Component implements OnInit {
    public forecasts: WeatherForecast[];

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string,
        private oauthService: OAuthService) { }

    ngOnInit(): void {
        var headers = new Headers({
            "Authorization": "Bearer " + this.oauthService.getAccessToken()
        });

        this.http.get(this.baseUrl + 'api-31/WeatherForecast',
            { headers: headers })
            .subscribe(result => {
                this.forecasts = result.json() as WeatherForecast[];
            }, error => console.error(error));
    }
}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}
