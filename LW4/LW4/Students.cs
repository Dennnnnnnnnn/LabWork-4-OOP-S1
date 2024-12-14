namespace LW4
{
    /// <summary>
    /// Class to store data about students
    /// </summary>
    internal class Students
    {
        const int CMax = 100; // Maximum number of student
        private Student[] students; // Object collection to store all students
        private int numberOfStudents; // Number of students

        /// <summary>
        /// Constructor without parameters (default)
        /// </summary>
        public Students()
        {
            numberOfStudents = 0;
            students = new Student[CMax];
        }
        /// <summary>
        /// Gets number of students
        /// </summary>
        /// <returns> Number of students</returns>
        public int GetNumberOfStudents()
        {
            return numberOfStudents;
        }
        /// <summary>
        /// Gets student
        /// </summary>
        /// <param name="index"> Index of student</param>
        /// <returns> Student</returns>
        public Student GetStudent(int index)
        {
            return students[index];
        }
        /// <summary>
        /// Adds student
        /// </summary>
        /// <param name="st">Student</param>
        public void AddStudent(Student st)
        {
            if (numberOfStudents < CMax)
            {
                students[numberOfStudents++] = st;
            }
        }
        /// <summary>
        /// Sets student to specified index
        /// </summary>
        /// <param name="st"> Student</param>
        /// <param name="ind"> Index</param>
        public void SetStudent(Student st, int ind)
        {
            if (ind < CMax && ind >= 0)
            {
                students[ind] = st;
            }
        }
        /// <summary>
        /// Sets number of students
        /// </summary>
        /// <param name="number"> Number of students</param>
        public void SetNumberOfStudents(int number)
        {
            numberOfStudents = number;
        }
        /// <summary>
        /// Removes student
        /// </summary>
        /// <param name="ind"> Index of student</param>
        public void RemoveStudent(int ind)
        {
            if (ind == numberOfStudents - 1)
            {
                students[ind] = null;
            }
            else
            {
                for (int i = ind; i < numberOfStudents; i++)
                {
                    students[i] = students[i + 1];
                }
            }
            numberOfStudents--;
        }
    }
}
