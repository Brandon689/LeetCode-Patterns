using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode_Patterns
{
    public class Sql
    {
        public void AddSolution(SQLiteConnection db, Solution solution)
        {
            db.Insert(solution);
        }

        public int AddProblem(SQLiteConnection db, Problem p)
        {
            db.Insert(p);
            return p.Id;
        }

        public Problem GetProblemById(SQLiteConnection db, int id)
        {
            var query = db.Table<Problem>().Where(v => v.Id == id);
            return query.FirstOrDefault();
        }

        public Solution GetSolutionById(SQLiteConnection db, int id)
        {
            var query = db.Table<Solution>().Where(v => v.ProblemId == id);
            return query.FirstOrDefault();
        }

        public List<Problem> GetAllProblems(SQLiteConnection db)
        {
            var query = db.Table<Problem>();
            return [.. query];
        }

        public List<Solution> GetAllSolutions(SQLiteConnection db)
        {
            var query = db.Table<Solution>();
            return [.. query];
        }
    }
}
