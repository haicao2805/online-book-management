using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class Helper
    {
        public static double GetProductPriceBaseOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity < 50)
            {
                return price;
            }
            else
            { 
                if (quantity < 100)
                {
                    return price50;
                }
                else
                {
                    return price100;
                }
            } 

        }

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
