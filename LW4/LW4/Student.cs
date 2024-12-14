using System;

namespace LW4
{
    /// <summary>
    /// Class to store data about student
    /// </summary>
    internal class Student
    {
        private string surname; // Student surname
        private string name; // Student name
        private string faculty; // Student faculty
        private string specialty; // Student specialty
        /// <summary>
        /// Constructor without parameters (default)
        /// </summary>
        public Student()
        {
            surname = "";
            name = "";
            faculty = "";
            specialty = "";
        }
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="surname"> Student surname</param>
        /// <param name="name"> Student name</param>
        /// <param name="faculty"> Student faculty</param>
        /// <param name="specialty"> Student specialty</param>
        public Student(string surname, 
            string name, string faculty, string specialty)
        {
            this.surname = surname;
            this.name = name;
            this.faculty = faculty;
            this.specialty = specialty;
        }
        /// <summary>
        /// Gets student surname
        /// </summary>
        /// <returns> Student surname</returns>
        public string GetSurname()
        {
            return surname;
        }
        /// <summary>
        /// Gets student name
        /// </summary>
        /// <returns> Student name</returns>
        public string GetName()
        {
            return name;
        }
        /// <summary>
        /// Gets student faculty
        /// </summary>
        /// <returns> Student faculty</returns>
        public string GetFaculty()
        {
            return faculty;
        }
        /// <summary>
        /// Gets student specialty
        /// </summary>
        /// <returns> Student specialty</returns>
        public string GetSpecialty()
        {
            return specialty;
        }
        /// <summary>
        /// Sets student surname
        /// </summary>
        /// <param name="surname"> Surname</param>
        public void SetSurname(string surname)
        {
            this.surname = surname;
        }
        /// <summary>
        /// Sets student name
        /// </summary>
        /// <param name="name"> Name</param>
        public void SetName(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// Sets student faculty
        /// </summary>
        /// <param name="faculty"> Faculty</param>
        public void SetFaculty(string faculty)
        {
            this.faculty = faculty;
        }
        /// <summary>
        /// Sets student specialty
        /// </summary>
        /// <param name="specialty"> Specialty</param>
        public void SetSpecialty(string specialty)
        {
            this.specialty = specialty;
        }
        /// <summary>
        /// Overloaded operator '>' to compare students 
        /// faculties in alphabetical order
        /// </summary>
        /// <param name="st1"> Student 1</param>
        /// <param name="st2"> Student 2</param>
        /// <returns> True if student 1 faculty 
        /// is bigger that student 2 faculty, false otherwise</returns>
        public static bool operator >(Student st1, Student st2)
        {
            int p = String.Compare(st1.GetFaculty(), 
                st2.GetFaculty(), StringComparison.CurrentCulture);

            return p > 0;
        }
        /// <summary>
        /// Overloaded operator '<' to compare students 
        /// faculties in alphabetical order
        /// </summary>
        /// <param name="st1"> Student 1</param>
        /// <param name="st2"> Student 2</param>
        /// <returns> True if student 1 faculty 
        /// is smaller that student 2 faculty, false otherwise</returns>
        public static bool operator <(Student st1, Student st2)
        {
            int p = String.Compare(st1.GetFaculty(), 
                st2.GetFaculty(), StringComparison.CurrentCulture);

            return p < 0;
        }
    }
}
