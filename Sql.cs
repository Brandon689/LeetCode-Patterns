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

        public void AddProblem(SQLiteConnection db, Problem p)
        {
            db.Insert(p);
        }

        public Problem a(SQLiteConnection db)
        {
            var query = db.Table<Problem>().Where(v => v.Id == 1);
            return query.FirstOrDefault();
        }
    }
}
