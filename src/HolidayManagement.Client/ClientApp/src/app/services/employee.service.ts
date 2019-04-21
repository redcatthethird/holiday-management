import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../config/config';
import { BaseService } from './base.service';
import { Employee } from '../models';
import { map } from 'rxjs/operators';

@Injectable()
export class EmployeeService extends BaseService {
    private pathApi = this.config.setting['PathApi'];
    public errorMessage: string;
    constructor(private http: HttpClient, private config: AppConfig) {
        super();
    }

    getEmployee(id: number) {
        return this.http.get<Employee>(
            `${this.pathApi}/employees/${id}`,
            super.header())
            .pipe(map(super.extractData));
    }
    
    getEmployees() {
        return this.http.get<Employee[]>(
            `${this.pathApi}/employees`,
            super.header())
            .pipe(map(super.extractData));
    }
}