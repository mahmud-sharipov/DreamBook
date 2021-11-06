import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationItemModel } from 'src/app/navigation/NavigationItemModel';
import { MENU } from '../../navigation/navigation'
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit, OnDestroy {

  menu: NavigationItemModel[] = MENU;

  constructor() { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.menu = [];
  }

}
