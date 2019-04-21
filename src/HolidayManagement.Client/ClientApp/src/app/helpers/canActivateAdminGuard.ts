import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Helpers } from './helpers';

@Injectable()
export class AdminGuard implements CanActivate {
    constructor(private helper: Helpers) {}

    canActivate() {
        return this.helper.isAdmin();
    }
}