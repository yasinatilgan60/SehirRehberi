﻿using SehirRehberi_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi_API.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T:class; // T class tip olmalıdır.
        void Delete<T>(T entity) where T : class; // T class tip olmalıdır.
        bool SaveAll();

        List<City> GetCities();
        List<Photo> GetPhotosByCity(int cityId);
        City GetCityById(int cityId);
        Photo GetPhoto(int id);
    }
}
