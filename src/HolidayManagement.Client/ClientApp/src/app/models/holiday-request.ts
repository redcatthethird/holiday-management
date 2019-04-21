import { Employee } from "./employee";

export class HolidayRequest {
    id: number;
    startDate: string;
    endDate: string;
    status: HolidayRequestStatus;
    comments?: string;
    employeeId: number;
    employee?: Employee;
}

export enum HolidayRequestStatus {
    pending = 0,
    approved = 1,
    refused = 2
}