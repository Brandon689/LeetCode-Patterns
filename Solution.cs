using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode_Patterns
{
    public class Solution
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Price { get; set; }
    }

    public class Problem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ProblemHTML { get; set; }
    }
}
