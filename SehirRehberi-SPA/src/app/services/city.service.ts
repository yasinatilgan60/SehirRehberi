import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { City } from '../models/city';
import { Observable, Subscriber } from 'rxjs';
import { Photo } from '../models/photo';
import { AlertifyService } from './alertify.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: "root"
})
export class CityService {
  constructor(private httpClient: HttpClient, private alertifyService: AlertifyService,
    private router: Router) {}
  // servisin yolu belirtilir.
  path = "http://localhost:51979/api"

  getCities():Observable<City[]>{
    return this.httpClient.get<City[]>(this.path+"/cities");
  }
  getCityById(cityId):Observable<City>{
    return this.httpClient.get<City>(this.path+"/cities/detail/?id="+cityId)
  }
  getPhotosByCity(cityId):Observable<Photo[]>{
    return this.httpClient.get<Photo[]>(this.path+"/cities/photos/?id="+cityId)
  }
  add(city){
    this.httpClient.post(this.path+'/cities/add',city).subscribe(data => {
      this.alertifyService.success("Şehir başarıyla eklendi."),
      this.router.navigateByUrl('/cityDetail/'+data["id"])
    });
  }
}
