import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { map } from "rxjs/operators";

@Injectable()
export class DataService {

    constructor(private http: Http) {

    }

    public products = [];

    public loadProducts() {
        return this.http.get("/api/products")
            .map((result: Response) => this.products = result.json());
            
    }
}