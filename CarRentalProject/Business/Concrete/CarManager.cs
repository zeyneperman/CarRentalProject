using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Business;
using Core.Results;
using Core.Utulities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager :ICarService
    {
        // bir iş sınıfı başka sınıflardan yeni eleman üretmez
        // burada sadece ICarDal kullanılır, ayni soyut nesneyle bağlantı kurulur
        ICarDal _carDal;
        IBrandService _brandService;
        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryLimitExceded(),
                CheckIfCarCountOfBrandCorrect(car.BrandId));
            if (result != null)
            {
                return result;
            }
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
        public IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result > 20)
            {
                return new ErrorResult(Messages.CarCountOfBrandError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded() // CategoryService ekelmememizin seebebi şu; Categoryden aldığın değeri nasıl yorumlayacağımı ele aldım.
        {
            var result = _brandService.GetAll();
            if (result.Data.Count > 50)
            {
                return new ErrorResult(Messages.BrandLimitExceted);
            }
            return new SuccessResult();
        }
    }
}
