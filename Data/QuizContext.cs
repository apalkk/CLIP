using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QA_Feedback.Models;

    public class QuizContext : DbContext
    {
        public int Curr { get; set; } = 0;
        public QuizContext (DbContextOptions<QuizContext> options)
            : base(options)
        {
        }

        public DbSet<QA_Feedback.Models.Question> Question { get; set; } = default!;

        public DbSet<QA_Feedback.Models.Rating> Rating { get; set; } = default!;

        public DbSet<QA_Feedback.Models.Source> Source { get; set; } = default!;
    }
