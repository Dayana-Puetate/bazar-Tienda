using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webIntegradorSeptimo.Negocio
{
    public class Validadores
    {

        public decimal convertDecimal(string numero)
        {
            decimal res= 0;
            try
            {
                res = Decimal.Parse(numero.Replace(".", ","));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                
            }

            return res;
        }


    }
}