namespace Project02
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string ScoresPath = @"../Resources/averageData/scores.json";
            const string StudentsPath = @"../Resources/averageData/students.json";

            const int NumberToPrint = 3;

            DataReader dataReader = new DataReader();
            var scoreLogs = dataReader.Read<ScoreLog>(ScoresPath);
            var students = dataReader.Read<Student>(StudentsPath);
            
            var result = new AverageProcess().Process(students, scoreLogs);
            var resultPrinter = new ResultPrinter(NumberToPrint);
            resultPrinter.Print(result);
        }
    }
}