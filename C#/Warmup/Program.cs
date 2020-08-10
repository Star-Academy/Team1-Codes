using System;
using System.Linq;

namespace Project02
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string scoresPath = "scores.json";
            const string studentsPath = "students.json";

            var scoreLogs = ReadData.Read<ScoreLog>(scoresPath);
            var students = ReadData.Read<Student>(studentsPath);

            if (scoreLogs == null || students == null) return;

            var scores = scoreLogs.GroupBy(scoreLog => scoreLog.StudentNumber)
                .Select(group => new
                {
                    StudentId = group.Key,
                    Average = group.Average(y => y.Score)
                });

            var result = students.Join(scores,
                    student => student.StudentNumber,
                    score => score.StudentId,
                    (student, score) => new
                    {
                        StudentName = student.FirstName + " " + student.LastName,
                        score.Average
                    })
                .OrderByDescending(x => x.Average).ToList();

            foreach (var student in result.Take(3))
            {
                Console.WriteLine(student.StudentName + " " + student.Average);
            }
        }
    }
}