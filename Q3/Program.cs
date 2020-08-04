using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q3
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("How many random numbers you want generate?: ");

         var val = Console.ReadLine();

         // convert to integer
         int n = Convert.ToInt32(val);

         List<int> randomNumbers = GetRandoms(n);
         PrintRandomNumbers(randomNumbers);
         randomNumbers = SortRandoms(randomNumbers);
         Console.WriteLine("Input n Value: ");
         val = Console.ReadLine();

         // convert to integer
         n = Convert.ToInt32(val);

         Console.WriteLine("nth smallest value in random list is : " + randomNumbers[n - 1]);
         Console.ReadLine();

      }

      /// <summary>
      /// Prints all generated random numbers
      /// </summary>
      /// <param name="randomNumbers"></param>
      private static void PrintRandomNumbers(List<int> randomNumbers)
      {
         Console.WriteLine("generated Random numbers are ");

         foreach (int n in randomNumbers)
         {
            Console.Write(n + " ");
         }
         Console.WriteLine("");
      }

      /// <summary>
      /// Sorts the generated random numbers
      /// </summary>
      /// <param name="randomNumbers"></param>
      /// <returns></returns>
      private static List<int> SortRandoms(List<int> randomNumbers)
      {
         randomNumbers.Sort();
         return randomNumbers;
      }

      /// <summary>
      /// Generates Random numbers
      /// </summary>
      /// <param name="count"></param>
      /// <returns></returns>
      static List<int> GetRandoms(int count)
      {
         Random random = new Random();
         List<int> result = new List<int>(count);
         for (int i = 0; i < count; i++)
         {
            result.Add(random.Next(1,10000));
         }
         return result;
      }
   }
}
