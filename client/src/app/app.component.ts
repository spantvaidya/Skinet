import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'Skinet';
products: any[]=[];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('https://localhost:7151/api/Products?pageSize=58').subscribe({
      next: (response : any) => this.products = response.data, //what to do 
      error:error => console.log(error), //what to do if error
      complete:() => {
        console.log('request completed');
        console.log('extra statement');
      }
    })
  }
}

