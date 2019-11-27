# SehirRehberi
 Udemy üzerinden aldığım AngularJS eğitiminde .NET Core ve AngularJS ile geliştirmiş olduğum projedir. Gerekli görülen yerlerde yorum satırları ile açıklama yapılmıştır.

# SehirRehberi_API - Backend

AutoMapper, AutoMapper (DependencyInjection), CloudinaryDotNet eklentileri manage nuget ile yüklenmiştir.
Proje çalıştırılmadan önce **appsetting.json** içerisindeki **CloudinarySettings, ConnectionStrings ve AppSettings** alanlarının tanımlanması gerekmektedir.

# SehirRehberi-SPA - Frontend

Bootswatch, ng2-file-upload, ngx-bootstrap, ngx-gallery, ngx-editor eklentileri yüklenmiştir. Token işlemleri için AngularJwt yüklenmiştir.

```
npm install
npm i bootswatch
npm install angular2-jwt
npm install ngx-bootstrap --save
npm install font-awesome --save
npm install ngx-editor --save
npm install angular2-jwt
```
Services içerisinde backend ile bağlantının sağlandığı api adresinin (path) projenin çalıştırıldığı api adresine göre güncellenmesi gerekmektedir.
