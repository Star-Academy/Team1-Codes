namespace Project02
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string ScoresPath = "data\\scores.json";
            const string StudentsPath = "data\\students.json";

            const int NumberToPrint = 3;

            DataReader dataReader = new DataReader();
            var scoreLogs = dataReader.Read<ScoreLog>(ScoresPath);
            var students = dataReader.Read<Student>(StudentsPath);

            new ResultPrinter(NumberToPrint).Print(new AverageProcess().Process(students, scoreLogs));
        }
    }
}