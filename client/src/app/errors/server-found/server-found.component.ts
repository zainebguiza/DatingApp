import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-found',
  templateUrl: './server-found.component.html',
  styleUrls: ['./server-found.component.css']
})
export class ServerFoundComponent implements OnInit {
  error:any;
  
  constructor(private router:Router) { 
    const navigation=this.router.getCurrentNavigation();
    this.error=navigation?.extras?.state?.['error'];
    
  }

  ngOnInit(): void {
  }

}
