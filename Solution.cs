﻿using SQLite;
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
        public int ProblemId { get; set; }
        public int CategoryId { get; set; }
        public string Path { get; set; }
    }
}
