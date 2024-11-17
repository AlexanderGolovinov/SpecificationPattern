import {Component, inject, OnInit} from '@angular/core';
import {ProductService} from "../../services/product.service";
import {ShopParams} from "../../../../shared/models/shop-params";
import {Pagination} from '../../../../shared/models/pagination';
import {Product} from "../../../../shared/models/product";
import {MatListOption, MatSelectionList, MatSelectionListChange} from "@angular/material/list";
import {MatMenu, MatMenuTrigger} from "@angular/material/menu";
import {MatIcon} from "@angular/material/icon";
import {FormsModule} from "@angular/forms";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {MatDialog} from "@angular/material/dialog";
import {FilterDialogComponent} from "../ui/filter-dialog/filter-dialog.component";

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

  private dialogService = inject(MatDialog);
  private productService = inject(ProductService);

  ngOnInit() {
    this.initialiseShop();
  }

  initialiseShop() {
    this.productService.getTypes();
    this.productService.getBrands();
    this.getProducts();
  }

  resetFilters() {
    this.shopParams = new ShopParams();
    this.getProducts();
  }

  getProducts() {
    this.productService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: error => console.error(error)
    })
  }

  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getProducts();
    }
  }

  openFiltersDialog() {
    const dialogRef = this.dialogService.open(FilterDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    });
    dialogRef.afterClosed().subscribe({
      next: result => {
        if (result) {
          this.shopParams.brands = result.selectedBrands;
          this.shopParams.types = result.selectedTypes;
          this.shopParams.pageNumber = 1;
          this.getProducts();
        }
      }
    })
  }
}
