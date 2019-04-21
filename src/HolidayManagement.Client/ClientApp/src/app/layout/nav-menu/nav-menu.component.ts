import { Component, OnInit } from '@angular/core';
import { Helpers } from 'src/app/helpers/helpers';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  
  isExpanded = false;
  admin = false;

  constructor(private helper: Helpers) { }
  
  ngOnInit() {
    this.admin = this.helper.isAdmin();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
