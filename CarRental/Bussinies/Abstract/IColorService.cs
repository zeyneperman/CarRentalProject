using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businiess.Abstract
{
    public interface IColorService
    {
        List<Color> GetAll();
        Color GetById(int categoryId);
    }
}
