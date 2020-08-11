using System;
using System.Linq;

namespace Project02
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string ScoresPath = "data\\scores.json";
            const string StudentsPath = "data\\students.json";

            ReadData readData = new ReadData();
            var scoreLogs = readData.Read<ScoreLog>(ScoresPath);
            var students = readData.Read<Student>(StudentsPath);

            if (scoreLogs == null || students == null) return;

            new PrintResult().Print(new AverageProcess().Process(students, scoreLogs));
        }
    }
}