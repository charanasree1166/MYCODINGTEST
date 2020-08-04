using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
   class Program
   {
      static void Main(string[] args)
      {
         string file = @".//..//..//..//Dict.txt";
         if (DoesFileExist(file))
         {
            Console.WriteLine("File Present at location" + file);
            PrintDict(file);
            Console.ReadLine();
         }
         else
         {
            Console.WriteLine("File Not Present at location" + file);
         }
      }

      /// <summary>
      /// To print the Dictionary words and meanings
      /// </summary>
      /// <param name="file">user file name</param>
      private static void PrintDict(string file)
      {
         string[] lines = File.ReadAllLines(file, Encoding.UTF8);
         foreach (string line in lines)
         {
            string[] words = line.Split('–');
            Console.WriteLine(words[0].Trim());
            string[] meanings = words[1].Split(',');
            foreach (string meaning in meanings)
            {
               Console.WriteLine(meaning.Trim());
            }
         }
      }

      /// <summary>
      /// To check the file exist or not
      /// </summary>
      /// <param name="fileName">User file name</param>
      /// <returns>returns true if file exists</returns>
      private static bool DoesFileExist(string fileName)
      {
         return File.Exists(fileName);
         // throw new NotImplementedException();
      }
   }
}
