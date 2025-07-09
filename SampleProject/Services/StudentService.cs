using ASP.NETWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETWebAPI.Services
{
    public class StudentService
    {
        private static List<StudentInfo> _students = new List<StudentInfo>
       {
            new StudentInfo { studentId = 1, name = "John Doe", mark = 85.5 },
            new StudentInfo { studentId = 2, name = "Jane Smith", mark = 92.0 },
            new StudentInfo { studentId = 3, name = "Sam Brown", mark = 78.0 },
            new StudentInfo { studentId = 4, name = "James ", mark = 92.0 },
            new StudentInfo { studentId = 5, name = "MexWell", mark = 78.0 }
        };

       

        internal List<StudentInfo> GetAllStudents()
        {
            return _students;
        }
        internal StudentInfo GetStudentById(int stdId)
        {
            if(stdId <= 0)
            {
                throw new ArgumentException("Invalid student ID.");
            }
            var student = _students.FirstOrDefault(s => s.studentId == stdId);
            if (student == null)
                throw new KeyNotFoundException($"Student with ID {stdId} not found.");
            return student;
        }
        internal List<StudentInfo> StudentWithAboveSpecifiedMarks(double mark)
        {
            if(mark<0 || mark>100)
                throw new ArgumentOutOfRangeException($" {mark} is not a valid mark. It should be between 0 and 100.");
            var students = _students.Where(s => s.mark > mark).ToList();
            if (students.Count == 0)
                throw new KeyNotFoundException($"No students found with marks above {mark}.");
            return students;
        }
        internal void AddStudent(StudentInfo student)
        {
            _students.Add(student);
        }

        internal int UpdateStudentInfo(StudentInfo updatingstudentinfo)
        {
            var studentIndex = _students.FindIndex(s => s.studentId == updatingstudentinfo.studentId);
            if (studentIndex < 0)
            {
                throw new KeyNotFoundException($"Student with ID {updatingstudentinfo.studentId} not found.");
            }
            _students[studentIndex] = updatingstudentinfo;
            return studentIndex;
        }

        internal object UpdateMarks(int id, double newMark)
        {
            var student = _students.FirstOrDefault(s => s.studentId == id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }
            student.mark = newMark;
            return student;

        }

        internal void Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.studentId == id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }
            _students.Remove(student);
           
        }
    }
}
