// TASK 3
// Data about students is stored in text file L4_3.txt.
// Number of students and number of month days is indicated in first line
// of a text file. Data about students is indicated in the following lines
// of a text file: student surname, name, faculty and specialty.
// Student lecture attendance information is given below: students (columns),
// number of missed lectures per day (rows). Store initial data to
// multidimensional container (1D and 2D). Determine if there were days when
// all students attended a lecture. If so, which days was it (display: day)?
// Sort student data is sorted by faculty, additionally sorting the appropriate
// columns in 2D array – matrix. Calculate how many lectures were missed by each
// faculty students. Find out if there are students that need to be expelled
// (did not attend lectures for more than half of specified days in a month).
// Expel such students, additionally expelling (removing) the appropriate
// columns in 2D array – matrix.
using System.Text;
using System.IO;
using System.Linq;

namespace LW4
{
    /// <summary>
    /// Primary class to execute required calculations tasks 
    /// </summary>
    internal class Program
    {
        const string data = "L4_3.txt"; // Data file
        const string result = "Result.txt"; //Result file
        static void Main(string[] args)
        {
            if (File.Exists(result))
            {
                File.Delete(result);
            }
            int numberOfStudents = 0; // Number of students
            int numberOfMonthDays = 0; // Number of days in month
            // Object collection to store students
            Students students = new Students();
            // 2D-array to store student attendance
            Matrix studentAttendance = Read(data, ref students,
                ref numberOfStudents, ref numberOfMonthDays);
            // Days when all students attended lectures
            string allStudentsAttendedDay = AllStudentsAttendanceDay
                (studentAttendance, students, numberOfMonthDays);

            PrintStudentsData(result, "Initial data", students, ref studentAttendance);
            PrintMatrix(result, studentAttendance);

            using (StreamWriter writer = new StreamWriter(result, true))
            {
                if ((allStudentsAttendedDay) != "")
                {
                    writer.WriteLine($"Days, when all students " +
                        $"attended lectures: {allStudentsAttendedDay}\n");
                }
                else
                {
                    writer.WriteLine("Day, when all students " +
                        "attended lectures was not found\n");
                }
            }

            Sort(ref students, ref studentAttendance);
            PrintStudentsData(result, "Sorted students data",
                students, ref studentAttendance);
            PrintMatrix(result, studentAttendance);

            // Calculations of missed lectures by each student
            int[] missedLectures = MissedLectures(students, studentAttendance);
            // Array that stores faculties
            string[] faculty = new string[numberOfStudents];
            // Array that stores missed lectures by each faculty
            int[] missedLecturesByEachFaculty;
            int numberOfFaculties = 0; // Number of faculties
            missedLecturesByEachFaculty = LecturesMissedByEachFaculty(
                students, studentAttendance, ref faculty, ref numberOfFaculties);
            PrintMissedLecturesByFaculty(
                result, faculty, missedLecturesByEachFaculty, numberOfFaculties);

            // Number of students who need to be expelled
            int numberOfStudentsToKick = 0;
            // Object collection to store students who need to be expelled
            Students studentsToKick = FindStudentsToKick
                (students, studentAttendance, ref numberOfStudentsToKick);
            PrintMissedLectures(result, "Students that need to be expelled",
                missedLectures, studentsToKick);

            ExpelStudents(ref students, studentsToKick, ref studentAttendance);
            PrintStudentsData(result, "New object collection (without students, " +
                "who skipped a lot of lectures): ",
                students, ref studentAttendance);
            PrintMatrix(result, studentAttendance);

        }
        /// <summary>
        /// Method to read data from text file
        /// </summary>
        /// <param name="students"> Object collection to store 
        /// data about students</param>
        /// <param name="numberOfStudents"> Number of students</param>
        /// <param name="numberOfMonthDays"> Number of days in month</param>
        /// <returns> 2D-array that stores attendance</returns>
        static Matrix Read(string datafile, ref Students students,
            ref int numberOfStudents, ref int numberOfMonthDays)
        {
            Matrix studentAttendance = new Matrix();
            using (StreamReader reader = 
                new StreamReader(datafile, Encoding.UTF8))
            {
                string[] parts;
                string line; // Stores line from text file
                int numberOfStudent = 0;
                line = reader.ReadLine();
                parts = line.Trim().Split(';');
                numberOfStudents = int.Parse(parts[0]);
                numberOfMonthDays = int.Parse(parts[1]);
                studentAttendance.numberOfRows = numberOfMonthDays;
                studentAttendance.numberOfColumns = numberOfStudents;
                string surname, // Student's surname
                        name,   // Student's name
                        faculty,  // Student's faculty
                        specialty;  // Student's specialty
                for (int i = 0; ((line = reader.ReadLine()) != null); i++)
                {
                    if (i < numberOfStudents)
                    {
                        parts = line.Trim().Split(';');
                        surname = parts[0];
                        name = parts[1];
                        faculty = parts[2];
                        specialty = parts[3];
                        // Creating new student
                        Student student = new Student
                            (surname, name, faculty, specialty);
                        students.AddStudent(student);
                    }
                    else
                    {
                        parts = line.Split(';');
                        for (int col = 0; col < numberOfStudents; col++)
                        {
                            studentAttendance.SetValue(numberOfStudent,
                                col, int.Parse(parts[col]));
                        }
                        numberOfStudent++;
                    }
                }
                return studentAttendance;
            }
        }
        /// <summary>
        /// Method to print data about students
        /// </summary>
        /// <param name="header"> Header</param>
        /// <param name="students"> Object collection that stores 
        /// data about students</param>
        /// <param name="studentAttendance"> 2D-array that stores 
        /// student attendance</param>
        static void PrintStudentsData(string result, string header,
            Students students, ref Matrix studentAttendance)
        {
            using (StreamWriter wr = new StreamWriter(result, true))
            {
                if (students.GetNumberOfStudents() != 0)
                {

                    wr.WriteLine(header);
                    wr.WriteLine($"Number of students: " +
                        $"{students.GetNumberOfStudents()}");
                    wr.WriteLine($"Number of days in month: " +
                        $"{studentAttendance.numberOfRows}");
                    wr.WriteLine("------------------------" +
                        "---------------------------");
                    wr.WriteLine("  {0, -10}   {1, -10}   {2, -10}   {3, -10}  ",
                        "Surname", "Name", "Faculty", "Specialty");
                    wr.WriteLine("-------------------------" +
                        "--------------------------");
                    for (int i = 0; i < students.GetNumberOfStudents(); i++)
                    {
                        Student st = students.GetStudent(i);
                        wr.WriteLine("| {0, -10} | {1, -10} | " +
                            "{2, -10} | {3, -7} |",
                            st.GetSurname(), st.GetName(),
                            st.GetFaculty(), st.GetSpecialty());
                    }
                    wr.WriteLine("--------------------------" +
                        "-------------------------");
                }
                else
                {
                    wr.WriteLine(header);
                    wr.WriteLine("This object collection is empty");
                }
            }
        }
        /// <summary>
        /// Method to print student attendance
        /// </summary>
        /// <param name="studentAttendance"> 2D-array to store attendance</param>
        static void PrintMatrix(string result, Matrix studentAttendance)
        {
            if (studentAttendance.numberOfColumns != 0)
            {
                using (StreamWriter wr = new StreamWriter(result, true))
                {
                    wr.WriteLine("Student attendance: ");
                    for (int row = 0; row < studentAttendance.numberOfRows; row++)
                    {
                        for (int col = 0;
                            col < studentAttendance.numberOfColumns; col++)
                        {
                            wr.Write("{0, 2}",
                                studentAttendance.GetValue(row, col));
                        }
                        wr.WriteLine("");
                    }
                    wr.WriteLine("");
                }
            }
        }
        /// <summary>
        /// Method to print missed lectures
        /// </summary>
        /// <param name="header"> Header</param>
        /// <param name="missedLectures"> Missed lectures</param>
        /// <param name="students"> Object collection that 
        /// stores data about students</param>
        static void PrintMissedLectures
            (string result, string header, 
            int[] missedLectures, Students students)
        {
            using (StreamWriter writer = new StreamWriter(result, true))
            {
                if (students.GetNumberOfStudents() != 0)
                {
                    writer.WriteLine(header);
                    writer.WriteLine("------------------------------" +
                        "--------------------------------------");
                    writer.WriteLine("  {0, -10}   {1, -10}   {2, -10}   " +
                        "{3, -10}  {4, -12}", "Surname", "Name", "Faculty",
                        "Specialty", "Missed lectures");
                    writer.WriteLine("------------------------------" +
                        "--------------------------------------");
                    for (int i = 0; i < students.GetNumberOfStudents(); i++)
                    {
                        Student st = students.GetStudent(i);
                        writer.WriteLine("| {0, -10} | {1, -10} | {2, -10} " +
                            "| {3, -10} | {4, -12} |", st.GetSurname(),
                            st.GetName(), st.GetFaculty(),
                            st.GetSpecialty(), missedLectures[i]);
                    }
                    writer.WriteLine("-------------------------------" +
                        "-------------------------------------\n");
                }
                else
                {
                    writer.WriteLine(header);
                    writer.WriteLine("This object collection is empty");
                }
            }
        }
        /// <summary>
        /// Finds days when all students attended lectures
        /// </summary>
        /// <param name="studentAttendance"> 2D-array that 
        /// stores student attendance</param>
        /// <param name="students"> Object collection that
        /// stores data about students</param>
        /// <param name="numberOfMonthDays"> Number of days in month</param>
        /// <returns> Days when all students attended lectures</returns>
        static string AllStudentsAttendanceDay
            (Matrix studentAttendance, Students students, int numberOfMonthDays)
        {
            string days = ""; //when lectures weren't skipped
            bool condition = true;
            for (int row = 0; row < numberOfMonthDays; row++)
            {
                for (int col = 0; col < students.GetNumberOfStudents()
                    && condition == true; col++)
                {
                    if (studentAttendance.GetValue(row, col) != 0)
                    {
                        condition = false;
                    }
                }
                if (condition)
                {
                    days += (row + 1).ToString() + " ";
                }
                condition = true;
            }
            return days.Trim();
        }
        /// <summary>
        /// Sorts object collection and 2D array by faculty
        /// </summary>
        /// <param name="students"> Object collection that stores 
        /// data about students</param>
        /// <param name="studentAttendance"> 2D-array that stores
        /// student attendance</param>
        static void Sort(ref Students students, ref Matrix studentAttendance)
        {
            int maxInd;
            int j;
            for (int i = 0; i < students.GetNumberOfStudents() - 1; i++)
            {
                maxInd = i;
                for (j = i + 1; j < students.GetNumberOfStudents(); j++)
                {
                    if (students.GetStudent(j) < students.GetStudent(maxInd))
                    {
                        maxInd = j;
                    }
                }
                Student temp = students.GetStudent(i);
                Student temp2 = students.GetStudent(maxInd);
                students.SetStudent(temp2, i);
                students.SetStudent(temp, maxInd);
                studentAttendance.SwapColumns(i, maxInd);
            }
        }
        /// <summary>
        /// Find how many lectures were missed by each student
        /// </summary>
        /// <param name="students"> Object collection that stores 
        /// data about students</param>
        /// <param name="studentAttendance"> 2D-array that stores 
        /// student attendance</param>
        /// <returns> How many lectures were missed by each student</returns>
        static int[] MissedLectures(Students students, Matrix studentAttendance)
        {
            int[] missedLectures = new int[students.GetNumberOfStudents()];
            for (int col = 0; col < studentAttendance.numberOfColumns; col++)
            {
                int sum = 0;
                for (int row = 0; row < studentAttendance.numberOfRows; row++)
                {
                    sum += studentAttendance.GetValue(row, col);
                }
                missedLectures[col] = sum;
            }
            return missedLectures;
        }
        /// <summary>
        /// Finds students that need to be expelled
        /// </summary>
        /// <param name="students"> Object collection that stores 
        /// data about students</param>
        /// <param name="studentAttendance"> 2D-array that stores 
        /// student attendance</param>
        /// <param name="counter"> Number of students to kick</param>
        /// <returns> Object collection of students 
        /// that need to be expelled</returns>
        static Students FindStudentsToKick
            (Students students, Matrix studentAttendance, ref int counter)
        {
            Students studentsToKick = new Students();
            for (int col = 0; col < students.GetNumberOfStudents(); col++)
            {
                int daysSkipped = 0;
                for (int row = 0; row < studentAttendance.numberOfRows; row++)
                {
                    if (studentAttendance.GetValue(row, col) != 0)
                    {
                        daysSkipped += 1;
                    }
                }
                if (daysSkipped > studentAttendance.numberOfRows / 2)
                {
                    Student temp = students.GetStudent(col);
                    studentsToKick.AddStudent(temp);
                    counter++;
                }
            }
            return studentsToKick;
        }
        /// <summary>
        /// Expels students who did not attend lectures 
        /// for more than half of specified days in a month
        /// </summary>
        /// <param name="students"> Object collection that stores 
        /// data about students</param>
        /// <param name="studentsToKick"> Object collection that 
        /// stores data about students who need to be expelled</param>
        /// <param name="studentAttendance"> 2D-array that stores 
        /// student attendance</param>
        static void ExpelStudents(ref Students students,
            Students studentsToKick, ref Matrix studentAttendance)
        {
            for (int i = 0; i < students.GetNumberOfStudents(); i++)
            {
                for (int j = 0; j < studentsToKick.GetNumberOfStudents(); j++)
                {
                    Student temp = studentsToKick.GetStudent(j);
                    if (temp == students.GetStudent(i))
                    {
                        students.RemoveStudent(i);
                        studentAttendance.RemoveColumn(i);
                    }
                }
            }
        }
        /// <summary>
        /// Calculates missed lectures by each faculty
        /// </summary>
        /// <param name="students"> Object collection that stores 
        /// data about students</param>
        /// <param name="studentAttendance">2D-array that stores 
        /// student attendance</param>
        /// <param name="faculty"> Object collection 
        /// that stores faculties</param>
        /// <param name="numberOfFaculties"> Number of faculties</param>
        /// <returns> Object collection that stores 
        /// missed lectures my each faculty</returns>
        static int[] LecturesMissedByEachFaculty(Students students, 
            Matrix studentAttendance, ref string[] faculty, 
            ref int numberOfFaculties)
        {
            int[] missedLecturesByEachStudent = 
                MissedLectures(students, studentAttendance);
            int[] missedLecturesByEachFaculty = 
                new int[students.GetNumberOfStudents()];
            string faculty1;
            numberOfFaculties = 0;
            int sum;
            for (int i = 0; i < students.GetNumberOfStudents(); i++)
            {
                faculty1 = students.GetStudent(i).GetFaculty();
                bool flag = false;
                sum = 0;
                for (int j = 0; j < students.GetNumberOfStudents(); j++)
                {
                    Student stud = students.GetStudent(j);
                    if (!(faculty.Contains(faculty1)) && 
                        (stud.GetFaculty() == faculty1))
                    {
                        sum += missedLecturesByEachStudent[j];
                        flag = true;
                    }
                }
                if (flag == true)
                {
                    faculty[numberOfFaculties] = faculty1;
                    missedLecturesByEachFaculty[numberOfFaculties++] = sum;
                }
            }
            return missedLecturesByEachFaculty;
        }
        /// <summary>
        /// Prints missed lectures by each faculty
        /// </summary>
        /// <param name="filename"> File to print data</param>
        /// <param name="faculty"> Object collection 
        /// that stores faculties</param>
        /// <param name="missedLecturesByEachFaculty"> Object collection 
        /// that stores missed lectures by each faculty</param>
        /// <param name="numberOfFaculties"> Number of faculties</param>
        static void PrintMissedLecturesByFaculty(string filename, 
            string[] faculty, int[] missedLecturesByEachFaculty,
            int numberOfFaculties)
        {
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                for (int i = 0; i < numberOfFaculties; i++)
                {
                    writer.WriteLine($"Faculty: {faculty[i]} " +
                        $"missed {missedLecturesByEachFaculty[i]} lectures");
                }
                writer.WriteLine("\n");
            }
        }
    }
}
