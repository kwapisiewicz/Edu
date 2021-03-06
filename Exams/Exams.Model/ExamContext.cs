﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Model
{
    public class ExamContext : DbContext
    {
        public ExamContext() : base("ExamDatabase")
        {
            Database.SetInitializer<ExamContext>(new ExamContextInitializer());
        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Score> Scores { get; set; }
    }
}
