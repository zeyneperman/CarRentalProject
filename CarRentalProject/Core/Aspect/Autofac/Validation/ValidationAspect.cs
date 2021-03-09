using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utulities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception("Doğrulama sınıfı değil.");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // Çalışmam anında instance oluşturmak istiyorsan Activator kullan
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // ProductValidator'un neyi new'leyeceğini söyler
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);// Metodun argümanlarını gez. 
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }


}
