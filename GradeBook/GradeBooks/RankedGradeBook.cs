using System;
using System.Collections.Generic;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("5 or more students required");
            }

            int threshold = (int)Math.Ceiling(Students.Count * 0.20);

            List<double> sortedGrades = SortedLetterGrades();

            if (sortedGrades[threshold - 1] <= averageGrade)
            {
                return 'A';
            } else if (sortedGrades[(threshold * 2) - 1] <= averageGrade)
            {
                return 'B';
            } else if (sortedGrades[(threshold * 3) - 1] <= averageGrade)
            {
                return 'C';
            } else if (sortedGrades[(threshold * 4) - 1] <= averageGrade)
            {
                return 'D';
            } else
            {
                return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.Write("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.Write("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }

        public List<double> SortedLetterGrades()
        {
            List<double> unSortedStudents = new List<double>();
            foreach (var student in Students)
            {
                unSortedStudents.Add(student.AverageGrade);
            }

            var sortedGrades = unSortedStudents.OrderByDescending(x => x);

            return sortedGrades.ToList();
        } 
    }
}