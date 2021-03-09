using Core.Utulities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businiess.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
    }
}
