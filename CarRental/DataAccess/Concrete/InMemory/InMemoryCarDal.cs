using Core.Utulities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car
                {
                    CarId = 1, BrandId = 1, ColorId = 3,
                    DailyPrice = 120, ModelYear = 2004},//,Description = "deneme"
                new Car
                {
                    CarId = 2, BrandId = 2, ColorId = 1,
                    DailyPrice = 300, ModelYear = 2010},
                new Car
                {
                    CarId = 3, BrandId = 2, ColorId = 4,
                    DailyPrice = 500, ModelYear = 2020},
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }
        public void Delete(Car car)
        {
            // LINQ / Language Integrated Query (Dile Gömmülü Sorgulama)
            // Liste bazlı yapıları SQL'deki gibi sorgulayabiliriz.
            Car carToDelete = null;
            // using System.Linq; yazmayı unutma
            // ürünleri tek tek dolaşmak için kullanılır
            // Id aramada gel-nelde "SingleOrDefault()" metodu kullanılır.
            // Bunun yerine "FirsstOrDefault()" veya "First()" metodlarını da kullanabiliriz.
            carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);

            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            throw new NotImplementedException();
        }


        public void Update(Car car)
        {
            //Göderilen ürün Id'sine sahip olan ürünü bul
            Car carToUpdate = null;
            carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            //carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }

        List<CarDetailDto> ICarDal.GetCarDetails()
        {
            throw new NotImplementedException();
        }
    }
}
