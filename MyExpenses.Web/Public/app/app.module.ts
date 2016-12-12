import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { FormsModule }    from '@angular/forms';
import { AppComponent } from './app.component';
import { RouterModule }   from '@angular/router';
import { DashboardComponent }   from './dashboard.component';

RouterModule.forRoot([
    {
        path: 'dashboard',
        component: DashboardComponent
    }
])


@NgModule({
    imports: [
        BrowserModule,
        FormsModule
    ],
    declarations: [
        AppComponent,
        DashboardComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
