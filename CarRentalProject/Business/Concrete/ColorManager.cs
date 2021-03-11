using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Business;
using Core.Results;
using Core.Utulities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorNameExists(color.ColorName));
            if (result != null)
            {
                return result;
            }
            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorNameExists(color.ColorName));
            if (result != null)
            {
                return result;
            }
            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        IDataResult<List<Color>> IColorService.GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorListed);
        }

        IDataResult<Color> IColorService.GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId));
        }
        private IResult CheckIfColorNameExists(string colorName)
        {
            var result = _colorDal.GetAll(c => c.ColorName == colorName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ColorNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
