import { HolidayRequest } from "./holiday-request";

export class Employee {
    id: number;
    name: string;
    role: EmployeeRole;
    holidayRequests?: HolidayRequest[];
}

export enum EmployeeRole {
    standard = 0,
    admin = 1
}