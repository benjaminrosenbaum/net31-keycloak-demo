import { Component, Inject, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
    selector: 'condense',
    templateUrl: './condense.component.html'
})
export class CondenseComponent implements OnInit {
    public user: CurrentUser = {givenName: 'nobody'};

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string,
        private oauthService: OAuthService) { }

    ngOnInit(): void {
        var headers = new Headers({
            "Authorization": "Bearer " + this.oauthService.getAccessToken()
        });

        this.http.get('http://localhost:5000/api/v1/users/current',
            { headers: headers })
            .subscribe((result: any) => {
                this.user = result.json() as CurrentUser;
            }, (error: any) => console.error(error));
    }
}

interface CurrentUser {
    givenName: string;
}
