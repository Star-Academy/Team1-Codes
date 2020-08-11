using System;
using System.Linq;
using System.Collections.Generic;

namespace Project02
{
    public class PrintResult {
        public void Print (List<ResultType> result){
            foreach (var student in result.Take(3))
            {
                Console.WriteLine(student.StudentName + " " + student.Average);
            }
        }
    }
}