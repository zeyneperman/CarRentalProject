using Core.Results;
using Core.Utulities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetById(int colorId);
        IResult Add(Customer customer);
        IResult Update(Customer customer);
    }
}
