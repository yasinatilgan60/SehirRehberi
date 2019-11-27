import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {NgxGalleryModule} from 'ngx-gallery';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { NavComponent } from './nav/nav.component';
import { CityComponent } from './city/city.component';
import { CityDetailComponent } from './city-detail/city-detail.component';
import {CityAddComponent} from './city/city-add/city-add.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'
import {AlertifyService} from './services/alertify.service';
import { RegisterComponent } from './register/register.component'
import {NgxEditorModule} from 'ngx-editor'
import {FileUploadModule} from 'ng2-file-upload'
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PhotoComponent } from './photo/photo.component';
@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      NavComponent,
      CityComponent,
      CityDetailComponent,
      CityAddComponent,
      RegisterComponent,
      PhotoComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      NgxGalleryModule,
      FormsModule, 
      ReactiveFormsModule,
      NgxEditorModule,
      TooltipModule.forRoot(),
      FileUploadModule
   ],
   providers: [AlertifyService],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
