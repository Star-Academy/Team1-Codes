using System.Linq;
using System.Collections.Generic;

namespace Project02
{
    public class AverageProcess
    {
        public List<ResultType> Process(List<Student> students, List<ScoreLog> scoreLogs)
        {
            return students.GroupJoin(scoreLogs,
                            student => student.StudentNumber,
                            scoreLog => scoreLog.StudentNumber,
                            (student, score) => new ResultType()
                            {
                                StudentName = student.FirstName + " " + student.LastName,
                                Average = score.Average(x => x.Score)
                            })
                        .OrderByDescending(x => x.Average).ToList();
        }
    }
}