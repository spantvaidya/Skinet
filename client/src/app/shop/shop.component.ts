import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{

  products:Product[] = [];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
        next: response => this.products = response.data, //what to do 
        error: error => console.log(error), //what to do if error
          complete: () => {
            console.log('request completed');
            console.log('extra statement');
          }
    })
  }

}
