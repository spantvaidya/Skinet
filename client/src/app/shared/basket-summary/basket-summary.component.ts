import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { BasketItem } from '../models/basket';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrl: './basket-summary.component.scss'
})
export class BasketSummaryComponent {
  @Output() addItem = new EventEmitter<BasketItem>();
  @Output() removeItem = new EventEmitter<{ id: number, quantity: number }>();
  @Input() isBasket = true;

  constructor(public basketService: BasketService) { }

  addBasketItem(item: BasketItem) {
    this.addItem.emit(item)
  }

  removeBasketItem(id: number, quantity = 1) {
    this.removeItem.emit({ id, quantity })
  }

}
