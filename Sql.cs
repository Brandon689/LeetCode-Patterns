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
        public static void AddStock(SQLiteConnection db, Solution solution)
        {
            db.Insert(solution);
        }
    }
}
