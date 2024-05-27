import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { OrderTotalsComponent } from './order-totals/order-totals.component';
import { StepperComponent } from './components/stepper/stepper.component';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    TextInputComponent,
    OrderTotalsComponent,
    StepperComponent,
    BasketSummaryComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    CarouselModule.forRoot(),
    CdkStepperModule,
    RouterModule
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    TextInputComponent,
    CarouselModule,
    OrderTotalsComponent,
    StepperComponent,
    CdkStepperModule,
    BasketSummaryComponent
  ]
})
export class SharedModule { }
