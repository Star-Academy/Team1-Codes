using System;
using System.Linq;
using System.Collections.Generic;

namespace Project02
{
    public class ResultPrinter
    {
        private int NumberToPrint;

        public ResultPrinter(int numberToPrint)
        {
            NumberToPrint = numberToPrint;
        }

        public void Print(List<ResultType> result)
        {
            foreach (var student in result.Take(NumberToPrint))
            {
                Console.WriteLine(student.StudentName + " " + student.Average);
            }
        }
    }
}