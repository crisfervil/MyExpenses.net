import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { FormsModule }    from '@angular/forms';
import { AppComponent } from './app.component';
import { RouterModule }   from '@angular/router';
import { DashboardComponent }   from './dashboard.component';
import { LoginComponent }   from './login.component';
import { ExpensesService }   from './expenses.service';
import { HttpModule }    from '@angular/http';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            {
                path: 'dashboard',
                component: DashboardComponent
            },
            {
                path: 'login',
                component: LoginComponent
            }
        ])
    ],
    declarations: [
        AppComponent,
        DashboardComponent, 
        LoginComponent
    ],
    providers: [ExpensesService],
    bootstrap: [AppComponent]
})
export class AppModule { }
