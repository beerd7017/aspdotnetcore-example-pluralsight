import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Router } from "@angular/router"
@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class Checkout {

  constructor(public data: DataService, private router: Router) {
  }

    onCheckout() {
        if (this.data.loginRequired) {
            this.router.navigate(["login"]);
        } else {
            this.router.navigate(["checkout"]);
        }
    }
}