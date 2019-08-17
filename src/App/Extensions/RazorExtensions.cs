using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace App.Extensions
{
    public static class RazorExtensions
    {
        public static string DocumentFormat(this RazorPage page, int personType, string documentNumber)
        {
            return personType == 1 ? Convert.ToUInt64(documentNumber).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(documentNumber).ToString(@"000\.000\.000\-00");
        }
    }
}
