import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/models/order';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../orders/order.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrl: './order-detailed.component.scss'
})
export class OrderDetailedComponent implements OnInit {
  order?: Order;
  constructor(private orderService: OrderService, private route: ActivatedRoute,
    private bcService: BreadcrumbService) {
    this.bcService.set('@OrderDetailed', ' ');
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    id && this.orderService.getOrderDetailed(+id).subscribe({
      next: order => {
        this.order = order;
        this.bcService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
      }
    })
  }

}
