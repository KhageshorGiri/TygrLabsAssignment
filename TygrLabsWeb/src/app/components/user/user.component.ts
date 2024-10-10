import { Component, OnInit } from '@angular/core';
import { Users } from '../../interfaces/user.interface';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit{

  allusers : Users[] = [];

  constructor() {
    // TODO: Add DI 
    
  }

  ngOnInit(): void {
     // TODO: ...
  }


}
