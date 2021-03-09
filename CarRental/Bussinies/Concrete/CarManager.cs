using Businiess.Abstract;
using Businiess.Constants;
using Businiess.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Results;
using Core.Utulities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businiess.Concrete
{
    public class CarManager : ICarService
    {
        // bir iş sınıfı başka sınıflardan yeni eleman üretmez
        // burada sadece ICarDal kullanılır, ayni soyut nesneyle bağlantı kurulur
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            
            _carDal.Add(car);
            return new SuccessResult(Messages.ProductAdded);
        }

        // iş kodları
        // koşulları buralarda kullanırız,
        public IDataResult<List<Car>> GetAll()
        {
            // iş kodları
            // koşulları buralarda kullanırız
            // işlemin gerçekleşip gerçekleşmediğine dair bir geri dönüş vermesini istiyorum

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.ProductListed);

        }

        public IDataResult<List<Car>> GetAllByBrand(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id));
        }

        public IDataResult<List<Car>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.DailyPrice <= min && p.DailyPrice <= max));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            // Aşagidaki yorum satırları consol denemesinde test etmek için olurşturuldu

            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }
        public IDataResult<Car> GetById(int cartId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.CarId == cartId));
        }
    }
}
