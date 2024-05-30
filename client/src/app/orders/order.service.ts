import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Order, OrderItem } from '../shared/models/order';
import { OrderParams } from '../shared/models/OrderParams';
import { Pagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOrdersForUser(orderParams: OrderParams) {
    let params = new HttpParams();

    params = params.append('pageIndex', orderParams.pageIndex);
    params = params.append('pageSize', orderParams.pageSize);
    if (orderParams.search) params = params.append('search', orderParams.search);

     return this.http.get<Pagination<Order[]>>(this.baseUrl + 'orders/', {params});
  }

  getOrderDetailed(id: number) {
    return this.http.get<Order>(this.baseUrl + 'orders/' + id);
  }
}
