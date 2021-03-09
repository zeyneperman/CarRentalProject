using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public interface IResult
    {
        // sadece getter'ları kullanıyoruz
        // kodlara dışardan erişimi kapatıyoruz
        bool Success { get;  }
        string Message { get; }
    }
}
