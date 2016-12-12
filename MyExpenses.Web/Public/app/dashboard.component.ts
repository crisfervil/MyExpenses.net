import { Component, OnInit } from '@angular/core';

import { Expense }        from './expense';
import { ExpensesService } from './expenses.service';

@Component({
    moduleId: module.id,
    selector: 'my-dashboard',
    templateUrl: 'dashboard.component.html',
    styleUrls: ['dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    expenses: Expense[] = [];

    constructor(private expenseService: ExpensesService) { }

    ngOnInit(): void {
        this.expenseService.getExpenses()
            .then(expenses => this.expenses = expenses.slice(1, 5));
    }
}