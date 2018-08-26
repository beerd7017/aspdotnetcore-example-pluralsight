import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs";
import { map } from 'rxjs/operators';
import { Product } from "./product";

@Injectable()
export class DataService {

    constructor(private http: Http) {

    }

    public products: Product[] = [];

    public loadProducts(): Observable<boolean> {
        return this.http.get("/api/products")
            .map((data: any[]) => {
                this.products = data;
                return true;
            });
    }
}