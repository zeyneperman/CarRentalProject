using Core.Results;
using Core.Utulities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IResult Add(User user);
        IDataResult<User> GetById(int userId);
        IResult Update(User user);
    }
}
