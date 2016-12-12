import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Expense } from './expense';

@Injectable()
export class ExpensesService {

    private headers = new Headers({ 'Content-Type': 'application/json' });
    private serviceUrl = 'api/expenses';  // URL to web api

    constructor(private http: Http) { }

    getExpenses(): Promise<Expense[]> {
        return this.http.get(this.serviceUrl)
            .toPromise()
            .then(response => response.json().data as Expense[])
            .catch(this.handleError);
    }


    getExpense(id: number): Promise<Expense> {
        const url = `${this.serviceUrl}/${id}`;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().data as Expense)
            .catch(this.handleError);
    }

    delete(id: number): Promise<void> {
        const url = `${this.serviceUrl}/${id}`;
        return this.http.delete(url, { headers: this.headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    create(name: string): Promise<Expense> {
        return this.http
            .post(this.serviceUrl, JSON.stringify({ name: name }), { headers: this.headers })
            .toPromise()
            .then(res => res.json().data)
            .catch(this.handleError);
    }

    update(expense: Expense): Promise<Expense> {
        const url = `${this.serviceUrl}/${expense.id}`;
        return this.http
            .put(url, JSON.stringify(expense), { headers: this.headers })
            .toPromise()
            .then(() => expense)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}