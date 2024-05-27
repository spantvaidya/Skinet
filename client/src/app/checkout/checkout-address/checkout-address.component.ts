import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AccountService } from '../../account/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrl: './checkout-address.component.scss'
})
export class CheckoutAddressComponent {
  @Input() checkoutForm?: FormGroup;

  constructor(private accountService: AccountService, private toastr: ToastrService) {}

  saveUserAddress() {
    this.accountService.updateUserAddress(this.checkoutForm?.get('addressForm')?.value).subscribe({
      next: () => {
        this.toastr.success('Address saved');
        this.checkoutForm?.get('addressForm')?.reset(this.checkoutForm?.get('addressForm')?.value);
      }
    })
  }
}
