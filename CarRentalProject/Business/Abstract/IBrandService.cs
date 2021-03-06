using Core.Results;
using Core.Utulities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBrandService
    { 
        IDataResult<List<Brand>> GetAll();
        IResult Add(Brand brand);
        IDataResult<Brand> GetById(int brandId);
        IResult Update(Brand brand);
    }
}
