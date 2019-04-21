import { Injectable } from "@angular/core";
import { HttpParams } from "@angular/common/http";

@Injectable()
export class Helpers {
    constructor() { }

    public isAdmin() {
        const key = 'admin';
        return this.hasQueryKey(key);
    }

    private hasQueryKey(key: string) {
        const url = location.href;
        if (!url.includes('?'))
            return false;

        const httpParams = new HttpParams({
            fromString: url.split('?')[1]
        });
        return httpParams.has(key);
      }
}