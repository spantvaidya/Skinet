import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/product';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  products: Product[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('https://localhost:7151/api/Products?pageSize=58').subscribe({
      next: response => this.products = response.data, //what to do 
      error: error => console.log(error), //what to do if error
      complete: () => {
        console.log('request completed');
        console.log('extra statement');
      }
    })
  }
}

