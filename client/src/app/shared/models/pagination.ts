
  export interface Pagination<T> {
    pageIndex: number;
    pageSize: number;
    pageCount: number;
    data: T;
  }