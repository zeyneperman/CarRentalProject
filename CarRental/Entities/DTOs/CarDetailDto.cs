using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarDetailDto :IDto
    {
        // Bu calss bir veri tabanı tablosunu temsil etmemektedir
        // Birden fazla tablonun join edilmiş hali için oluşturuldu
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }

    }
}
