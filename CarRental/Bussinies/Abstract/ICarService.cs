using Core.Results;
using Core.Utulities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businiess.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();//Ürün listeesi döndürüyor
        IDataResult<List<Car>> GetAllByBrand(int id);
        IDataResult<List<Car>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IResult Add(Car car);
        IDataResult<Car> GetById(int cartId);
    }
}
