import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from '../../basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { Basket } from '../../shared/models/basket';
import { OrderToCreate } from '../../shared/models/order';
import { Address } from '../../shared/models/user';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrl: './checkout-payment.component.scss'
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;
  loading = false;

  constructor(private basketService: BasketService, private checkoutService: CheckoutService,
    private toastr: ToastrService, private router: Router) { }

  submitOrder() {
    //debugger;
    //this.loading = true;
    const basket = this.basketService.getCurrentBasketValue();
    if (!basket) throw new Error('cannot get basket');
    try {
      const orderToCreate = this.getOrderToCreate(basket);
      if (!orderToCreate) return;
      this.checkoutService.createOrder(orderToCreate).subscribe({
        next: order => {
          debugger;
          this.toastr.success('Order created Successfully');
          this.basketService.deleteLocalBasket();
          const navigationExtras: NavigationExtras = { state: order };
          this.router.navigate(['checkout/success'], navigationExtras);
          //this.router.navigateByUrl('checkout/success');
        }
      })

    } catch (error: any) {
      console.log(error);
      this.toastr.error(error.message)
    } finally {
      //this.loading = false;
    }
  }

  private getOrderToCreate(basket: Basket) {
    //debugger;
    const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;
    if (!deliveryMethodId || !shipToAddress) return;
    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethodId,
      shipToAddress: shipToAddress
    }
  }
}

