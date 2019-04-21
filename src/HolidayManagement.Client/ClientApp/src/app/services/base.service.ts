import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class BaseService {
    constructor() { }

    public extractData(res: Response) {
        return res.json();
    }

    public header() {
        let header = new HttpHeaders({
            'Content-Type': 'application/json'
        });

        return { headers: header };
    }
 }