import { Component, Input, input } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrl: './paging-header.component.scss'
})
export class PagingHeaderComponent {
@Input() pageIndex? : number;
@Input() pageSize? : number;
@Input() totalCount? : number;
}
