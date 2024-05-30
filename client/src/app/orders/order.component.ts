import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Order } from '../shared/models/order';
import { OrderService } from './order.service';
import { OrderParams } from '../shared/models/OrderParams';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  orders: Order[] = [];
  orderParams = new OrderParams();
  totalCount = 0;

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.getOrders();
  }
  
  getOrders() {
    this.orderService.getOrdersForUser(this.orderParams).subscribe({
      next: response => {
        this.orders = response.data,
        this.orderParams.pageSize = response.pageSize,
        this.orderParams.pageIndex = response.pageIndex,
        this.totalCount = response.pageCount
      },
      error: error => console.log(error)
    })
  }

  onPageChanged(event: any) {
    if (this.orderParams.pageIndex !== event) {
      this.orderParams.pageIndex = event;
      this.getOrders();
    }
  }

  onSearch() {
    this.orderParams.search = this.searchTerm?.nativeElement.value;
    this.orderParams.pageIndex = 1;
    this.getOrders();
  }

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.orderParams = new OrderParams();
    this.getOrders();
  }

}
