import {Component, inject, OnInit} from '@angular/core';
import {ProductService} from "../../services/product.service";
import {ShopParams} from "../../../../shared/models/shop-params";
import {Pagination} from '../../../../shared/models/pagination';
import {Product} from "../../../../shared/models/product";
import {MatListOption, MatSelectionList} from "@angular/material/list";
import {MatMenu, MatMenuTrigger} from "@angular/material/menu";
import {MatIcon} from "@angular/material/icon";
import {FormsModule} from "@angular/forms";
import {MatPaginator} from "@angular/material/paginator";

@Component({
  selector: 'app-products-page',
  standalone: true,
  imports: [
    MatListOption,
    MatSelectionList,
    MatMenu,
    MatIcon,
    MatMenuTrigger,
    FormsModule,
    MatPaginator
  ],
  templateUrl: './products-page.component.html'
})
export class ProductsPageComponent implements OnInit {
  products?: Pagination<Product>;
  shopParams = new ShopParams();
  pageSizeOptions = [5, 10, 15, 20]

  private productService = inject(ProductService);

  ngOnInit(): void {
    this.initialiseShop();
  }

  initialiseShop() {
    this.productService.getTypes();
    this.productService.getBrands();
    this.getProducts();
  }

  getProducts(): void {
    this.productService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: error => console.error(error)
    })
  }

  // openFiltersDialog() {
  //   const dialogRef = this.dialogService.open(FiltersDialogComponent, {
  //     minWidth: '500px',
  //     data: {
  //       selectedBrands: this.shopParams.brands,
  //       selectedTypes: this.shopParams.types
  //     }
  //   });
  //   dialogRef.afterClosed().subscribe({
  //     next: result => {
  //       if (result) {
  //         this.shopParams.brands = result.selectedBrands;
  //         this.shopParams.types = result.selectedTypes;
  //         this.shopParams.pageNumber = 1;
  //         this.getProducts();
  //       }
  //     }
  //   })
  // }
  handlePageEvent($event: PageEvent) {

  }
}
