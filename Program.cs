//Marko Buha; Final Markbook Performance Task; June 23rd, 2021; For Ms. Khan
using System;
using System.Collections.Generic; //For lists
using System.IO; //For reading and writing to file
namespace CSharpSchool
{
    class Start : Student //First class
    {
        static void Main() //Entry point for code
        {
            Student s1 = new Student(); //Declaration of objects to represent courses
            Student s2 = new Student();
            Student s3 = new Student();
            Student s4 = new Student();
            Console.WriteLine("---------------------------------Welcome to Buha Industries' Five Star Markbook------------------------------------"); //Put outside loop to output only once
            Console.WriteLine("This program has been developed to add students (and their marks to all existing assignments) to up to 4 courses, find students, and delete students from courses");
            Console.WriteLine("This program can also add new weighed assignments to fill with marks for all added students");
            Console.WriteLine("This program also can conduct mathematical calculations after having found a student, along with deleting them or replacing their marks");
            Console.WriteLine("Beside the program.cs tab, you can find a set of text files to add student input to for each course. Please format them as follows");
            Console.WriteLine("Assignment Name 1,Assignment Name 2,...");
            Console.WriteLine("Assignment Weight 1,Assignment Weight 2,...");
            Console.WriteLine("Student Name 1,Student Birthday 1,Student Phone 1,Student Email 1,Student 1 Assignment 1 Mark,Student 1 Assignment 2 Mark...");
            Console.WriteLine("Student Name 2,Student Birthday 2,Student Phone 2,Student Email 2,Student 2 Assignment 1 Mark,Student 2 Assignment 2 Mark...");
            Console.WriteLine("You can also find files in the output directory that will be written to upon quitting the program. Thank you for using this software");
            bool quit = false;
            while (!quit) //While user does not want to quit
            {
                Console.WriteLine("Which course to deal with? Enter a number from 1-4 for the course number. E for one student's average across all courses or Q to quit the program.");
                try
                {
                    CourseID = Convert.ToChar(Console.ReadLine());
                }
                catch (FormatException)
                {
                    //do nothing
                }
                Console.Clear(); //Clear previous input
                if (CourseID == '1')
                {
                    s1.InputProcessor(s1); //Each course is represented by a separate object to prevent having to create 4 different sets of lists
                }
                else if (CourseID == '2')
                {
                    s2.InputProcessor(s2);
                }
                else if (CourseID == '3')
                {
                    s3.InputProcessor(s3);
                }
                else if (CourseID == '4')
                {
                    s4.InputProcessor(s4);
                }
                else if ((CourseID == 'E') || (CourseID == 'e')) //Overall all-corse average finder
                {
                    Console.WriteLine("Which student to find overall all-course average for?");
                    string name = Convert.ToString(Console.ReadLine());
                    name = name.ToUpper();
                    double totalMark = 0;
                    int totalCourses = 0;
                    int counter1 = s1.Find(false, name); //Get counter value
                    if (counter1 != -1) //If name found
                    {
                        double marks1 = s1.Mean(s1.marks[counter1]);
                        if (marks1 != -1) //Exclude courses with 0 assessments b/c they were not taken
                        {
                            totalMark += marks1;
                            totalCourses++;
                        }
                    }
                    int counter2 = s2.Find(false, name); //Get counter value
                    if (counter2 != -1) //If name found
                    {
                        double marks2 = s2.Mean(s2.marks[counter2]);
                        if (marks2 != -1) //Exclude courses with 0 assessments b/c they were not taken
                        {
                            totalMark += marks2;
                            totalCourses++;
                        }
                    }
                    int counter3 = s3.Find(false, name); //Get counter value
                    if (counter3 != -1) //If name found
                    {
                        double marks3 = s3.Mean(s3.marks[counter3]);
                        if (marks3 != -1) //Exclude courses with 0 assessments b/c they were not taken
                        {
                            totalMark += marks3;
                            totalCourses++;
                        }
                    }
                    int counter4 = s4.Find(false, name); //Get counter value
                    if (counter4 != -1) //If name found
                    {
                        double marks4 = s4.Mean(s4.marks[counter4]);
                        if (marks4 != -1) //Exclude courses with 0 assessments b/c they were not taken
                        {
                            totalMark += marks4;
                            totalCourses++;
                        }
                    }
                    if (totalCourses != 0) //Prevent output when no courses
                    {
                        double average = totalMark / totalCourses;
                        average = Math.Round(average, 2);
                        Console.WriteLine("Mean across all courses is {0}%", average);  //Round marks off to prevent unnecessary decimal places
                        Console.WriteLine("An average of NaN% signifies that there is a course with no marks whatsoever on record.");
                    }
                }
                else if ((CourseID == 'Q') || (CourseID == 'q')) //Quit code and write info into the files
                {
                    quit = true;
                    s1.FileWriter(s1, "Marks1.txt");
                    s2.FileWriter(s2, "Marks2.txt");
                    s3.FileWriter(s3, "Marks3.txt");
                    s4.FileWriter(s4, "Marks4.txt");
                }
            }
            Console.WriteLine("Program successfully exited");
            Console.ReadLine(); //Stop code
        }
    }
    class Student //Defining a class to add student information
    {
        public static char CourseID { get; set; } //Consistent Course ID for the file reader
        List<string> names = new List<string>(); //https://stackoverflow.com/a/22320755 has syntax for public list
        List<DateTime> birthdays = new List<DateTime>();
        public List<List<short>> marks = new List<List<short>>(); //https://stackoverflow.com/a/34599227 has syntax for creating/adding elements to 2D list which work identically to 2D arrays except for being dynamic and having slightly different syntax I believe
        List<string> assignmentNames = new List<string>();
        List<double> assignmentWeights = new List<double>();
        List<string> phones = new List<string>(); //Some people like to put dashes in phone numbers
        List<string> emails = new List<string>();
        public void InputProcessor(Student s) //Add marks from file into new course
        {
            bool quit = false;
            do
            {
                Console.WriteLine("Enter S to add a student, A to add an assignment, F to find a student for further processing, I to import marks and assignments from a file, or Q to exit to the course selection page."); //Returns t
                char input = 'B';
                try
                {
                    input = Convert.ToChar(Console.ReadLine());
                }
                catch (FormatException) //If not a char
                {
                    //do nothing
                }
                if ((input == 'A') || (input == 'a'))
                {
                    s.AddAssignment();
                }
                else if ((input == 'S') || (input == 's'))
                {
                    s.Naming(s);
                }
                else if ((input == 'F') || (input == 'f'))
                {
                    Console.WriteLine("Which name to find?");
                    string name = Convert.ToString(Console.ReadLine());
                    name = name.ToUpper(); //Keeping all names in one case helps with users that use a different case
                    s.Find(true, name);
                }
                else if ((input == 'I') || (input == 'i'))
                {
                    s.FileReader(); //Send to method
                }
                else if ((input == 'Q') || (input == 'q'))
                {
                    quit = true;
                }
            }
            while (!quit);
        }
        short MarkProcessor(string input) //Check validity of mark
        {
            short mark;
            try
            {
                mark = Convert.ToInt16(input);
            }
            catch (FormatException)
            {
                mark = -1;
            }
            catch (OverflowException)
            {
                mark = -1;
            }
            if ((mark > 100) || (mark < -1))
            {
                mark = -1;
            }
            return mark;
        }
        void FileReader() //Read in info from a file
        {
            int preexistingAssignmentCount = assignmentNames.Count; //Save count beforehand to avoid changes as AssignmentName list length changed
            StreamReader myReader = new StreamReader("Marks1.txt"); //Placeholder to make other sections of method think local variable is assigned
            if (CourseID == '2') //If second course
            {
                myReader = new StreamReader("Marks2.txt");
            }
            else if (CourseID == '3')
            {
                myReader = new StreamReader("Marks3.txt");
            }
            else if (CourseID == '4')
            {
                myReader = new StreamReader("Marks4.txt");
            }
            string line = "";
            line = myReader.ReadLine();
            List<string> input = new List<string>(line.Split(','));
            for (int j = 0; j < input.Count; j++) //Read in assignment names
            {
                if (input[j] != "") //Prevent addition of empty lines after commas
                {
                    input[j].ToUpper(); //Make input uppercase
                    for (int k = 0; k < assignmentNames.Count; k++)
                    {
                        if (assignmentNames[k] == input[j])
                        {
                            assignmentNames.RemoveAt(k); //Remove duplicates of assignments at the index
                            assignmentWeights.RemoveAt(k);
                            preexistingAssignmentCount--; //One less preexisting assignment to deal with
                            for (int l = 0; l < names.Count; l++)
                            {
                                marks[l].RemoveAt(k); //For each student, remove duplicate assignment mark
                            }
                        }
                    }
                    assignmentNames.Add(input[j]); //Add assignment name to array
                }
            }
            line = myReader.ReadLine(); //Read next line
            input = new List<string>(line.Split(',')); //Replace list with new row of input
            for (int j = 0; j < input.Count; j++)
            {
                try
                {
                    if (input[j] != "")
                    {
                        assignmentWeights.Add(Convert.ToDouble(input[j]));
                    }
                }
                catch (FormatException)
                {
                    assignmentWeights.Add(0);
                }
                catch (OverflowException) //If weight too large to fit in short
                {
                    assignmentWeights.Add(0);
                }
            }
            line = myReader.ReadLine(); //Read next line
            while (line != null)
            {
                input = new List<string>(line.Split(','));
                input[0] = input[0].ToUpper(); //Make uppercase for comparing method later
                for (int i = 0; i < names.Count; i++)
                {
                    if (input[0] == names[i]) //If name is duplicate
                    {
                        names.RemoveAt(i);
                        birthdays.RemoveAt(i);
                        phones.RemoveAt(i);
                        emails.RemoveAt(i);
                        marks.RemoveAt(i); //Remove the duplicate student's entire row
                    }
                }
                names.Add(input[0]); //Make input uppercase
                birthdays.Add(Convert.ToDateTime(input[1])); //Put input into the Date-Time data type
                phones.Add(input[2]);
                emails.Add(input[3].ToUpper()); //Emails are not case sensitive
                marks.Add(new List<short>()); //Make new row
                for (int i = 0; i < preexistingAssignmentCount; i++)
                {
                    marks[names.Count - 1].Add(-1); //Fill preexisting assignments with the invalid mark
                }
                for (int i = 4; i < input.Count; i++) //Fourth and onward terms have marks
                {
                    short mark = MarkProcessor(input[i]); //Check mark for validity
                    if (input[i] != "")
                    {
                        marks[names.Count - 1].Add(mark);
                    }
                }
                line = myReader.ReadLine();
            }
            Console.WriteLine("Student(s) imported successfully.");
            myReader.Close(); //Allow reuse by rest of operating system
        }
        public void FileWriter(Student s, string fileName)
        {
            StreamWriter myWriter = new StreamWriter(fileName); //Open a writer to write in marks into the file
            for (int i = 0; i < assignmentNames.Count; i++) //Write course info in a beautiful visual format
            {
                myWriter.Write(assignmentNames[i] + ", "); //Separate output with commas
            }
            myWriter.WriteLine(); //Move to next line
            for (int i = 0; i < assignmentWeights.Count; i++) //Write course info in a beautiful visual format
            {
                myWriter.Write(assignmentWeights[i] + ", ");
            }
            myWriter.WriteLine();
            for (int i = 0; i < names.Count; i++) //For the amount of students present
            {
                myWriter.Write(names[i] + ", ");
                myWriter.Write(birthdays[i] + ", ");
                Console.Write(phones[i] + " ");
                Console.Write(emails[i] + " ");
                for (int j = 0; j < marks[i].Count; j++) //For the amount of marks the student has
                {
                    myWriter.Write(marks[i][j] + ", "); //Write each mark per student in order
                }
                myWriter.WriteLine(); //Space
            }
            myWriter.Close(); //Close for reuse by system
        }
        void Naming(Student s) //Gather name and DOB for adding
        {
            Console.WriteLine("Name?");
            string name = Convert.ToString(Console.ReadLine());
            name = name.ToUpper(); //https://www.geeksforgeeks.org/c-sharp-tolower-method/#:~:text=In%20C%23%2C%20ToLower()%20is,example%2C%20special%20symbols%20remain%20unchanged. for syntax but all upper looks better to me
            int counter = 0;
            bool match = false;
            foreach (string t in names)
            {
                if (t == name) //If there is a duplicate in the name within the list
                {
                    Console.WriteLine("Existing student found. You may now change their marks.");
                    match = true;
                    s.Replace(counter);
                }
                counter++;
            }
            if (match == false) //Prevent use of this method if Replace() used
            {
                s.AddStudent(name);
            }
        }
        void AddAssignment() //Method to add assignments to lists
        {
            Console.WriteLine("Enter the name of a marked assignment to add");
            string name = Console.ReadLine();
            name = name.ToUpper();
            int counter = 0;
            bool match = false;
            foreach (string t in assignmentNames)
            {
                if (t == name) //If there is a duplicate in the assignment name within the list
                {
                    Console.WriteLine("Existing assignment found. You may now change its marks.");
                    ReplaceAssignment(counter);
                    match = true;
                }
                counter++;
            }
            if (!match)
            {
                assignmentNames.Add(name); //Add the assignment name to the list
                bool quit = false;
                do
                {
                    Console.WriteLine("Input how much this assignment is weighed");
                    try
                    {
                        assignmentWeights.Add(Convert.ToInt16(Console.ReadLine()));
                        quit = true;
                    }
                    catch (FormatException)
                    {
                        //do nothing
                    }
                    catch (OverflowException)
                    {
                        //do nothing
                    }
                } while (!quit);
                for (int i = 0; i < names.Count; i++) //Add assignment marks for however many names there are
                {
                    Console.WriteLine("Enter {0}'s mark (out of 100) to add to this assignment. Enter an invalid mark to signify no mark.", names[i]);
                    string input = Console.ReadLine();
                    short mark = MarkProcessor(input);
                    marks[i].Add(mark); //Append mark to the student's row
                }
            }
        }
        void ReplaceAssignment(int Counter)
        {
            bool quit = false;
            do
            {
                Console.WriteLine("Input how much this assignment is weighed"); //Replace assignment weight
                try
                {
                    assignmentWeights[Counter] = Convert.ToInt16(Console.ReadLine());
                    quit = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (!quit);
            for (int i = 0; i < names.Count; i++) //For how many student names exist for the relevant assignment
            {
                Console.WriteLine("Input a mark (without the percent sign) for student {0}. Input anything else if the course was not taken.", names[i]);
                string input = Console.ReadLine();
                short mark = MarkProcessor(input);
                marks[i][Counter] = mark; //Replace mark at each student's index for that assignment with new mark
            }
        }
        void AddStudent(string Name) //Method accessible to whole code for adding student names. Requires Name to be given from other class
        {
            bool quit = false;
            DateTime birthday = new DateTime(1);
            while (!quit) //Run until valid DOB provided
            {
                Console.WriteLine("What is the student's date of birth?");
                try
                {
                    birthday = Convert.ToDateTime(Console.ReadLine()); //Goes to catch statement if in wrong format
                    quit = true;
                    if ((DateTime.Now.Year < birthday.Year) || (1900 > birthday.Year)) //If birth year makes no sense
                    {
                        Console.WriteLine("The student is not born yet or is too old to be alive today. The date of birth has not been added.");
                        quit = false; //Prevent quitting
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Date of birth was not in valid format and has not been added.");
                }
            }
            names.Add(Name); //Adding info to lists
            birthdays.Add(birthday);
            marks.Add(new List<short>());
            for (int i = 0; i < assignmentNames.Count; i++) //For how many assignments there are
            {
                Console.WriteLine("Input a number to represent their mark on assignment {0} out of 100 (without the percent sign). Input anything else if the course was not taken.", assignmentNames[i]);
                string input = Console.ReadLine();
                Start s1 = new Start();
                short mark = s1.MarkProcessor(input);
                marks[names.Count - 1].Add(mark); //Add the mark to the most recently created name
            }
            Console.WriteLine("Student added successfully."); //Provide confirmation to user
        }
        public int Find(bool runCounter, string name) //Find student for further actions
        {
            bool quit = false; //Set quit checker variable
            for (int counter = 0; counter < names.Count; counter++) //Check all names in list for match using counter
            {
                if (name == names[counter]) //If match
                {
                    if (runCounter) //If need to run these processes
                    {
                        while (!quit) //Run loop until valid entry
                        {
                            int input = 'b';
                            Console.WriteLine("Press D to delete their entry, R to replace their marks, V to find their average, or Q to quit to the course selection page.");
                            try
                            {
                                input = Convert.ToChar(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                //do nothing
                            }
                            if (input == 'd' || input == 'D')
                            {
                                Delete(counter); //Send counter to delete name at that counter
                                quit = true; //Exit loop as actions on the deleted student can obviously not be done anymore
                            }
                            else if (input == 'r' || input == 'R')
                            {
                                Replace(counter);
                            }
                            else if (input == 'v' || input == 'V')
                            {
                                Console.WriteLine("{0}, who is {1} years of age, has a mean of {2}%, a median of {3}%, and a mode of {4}%", names[counter], GetAge(counter), Math.Round(Mean(marks[counter]), 2), Median(marks[counter]), Mode(marks[counter]));
                                Console.WriteLine("In this course, the class has a mean of {0}%, and a median of {1}%", Math.Round(ClassMean(), 2), Math.Round(ClassMedian(), 2)); //Things that can be decimals need rounding off to 2 decimal places
                                Console.WriteLine("Please note that -1% is equal to a course not taken or a course with no valid mark provided. An average of NaN% signifies that there are no valid marks on record.");
                                Console.Write("Assignments: ");
                                for (int i = 0; i < assignmentNames.Count; i++) //Output course info in a beautiful visual format
                                {
                                    Console.Write(assignmentNames[i] + " ");
                                }
                                Console.WriteLine(); //Move to next line
                                Console.Write("Weights: ");
                                for (int i = 0; i < assignmentWeights.Count; i++) //Output course info in a beautiful visual format
                                {
                                    Console.Write(assignmentWeights[i] + " ");
                                }
                                Console.WriteLine();
                                Console.WriteLine("Assignment Means: ");
                                for (int i = 0; i < assignmentNames.Count; i++) //Output course info in a beautiful visual format
                                {
                                    Console.Write(AssignmentMeans(i) + " ");
                                }
                                Console.WriteLine();
                                Console.WriteLine("Assignment Medians: ");
                                for (int i = 0; i < assignmentNames.Count; i++) //Output course info in a beautiful visual format
                                {
                                    Console.Write(AssignmentMedians(i) + " ");
                                }
                                Console.WriteLine();
                                Console.WriteLine("Assignment Modes: ");
                                for (int i = 0; i < assignmentNames.Count; i++) //Output course info in a beautiful visual format
                                {
                                    Console.Write(AssignmentModes(i) + " ");
                                }
                                Console.WriteLine();
                                for (int i = 0; i < names.Count; i++) //For the amount of students present
                                {
                                    Console.Write(names[i] + " ");
                                    Console.Write(birthdays[i] + " ");
                                    Console.Write(phones[i] + " ");
                                    Console.Write(emails[i] + " ");
                                    for (int j = 0; j < marks[i].Count; j++) //For the amount of marks the student has
                                    {
                                        Console.Write(marks[i][j] + " "); //Output each mark per student in order
                                    }
                                    Console.WriteLine(); //Space
                                }
                            }
                            else if (input == 'q')
                            {
                                quit = true;
                            }
                        }
                        break; //Exit as no need to keep running once found
                    }
                    return counter; //return value at which name was found
                }
            }
            if (!quit) //Prevent running if previous match
            {
                Console.WriteLine("Name of this student within this particular course not found.");
            }
            return -1; //If name never found, return value to represent that
        }
        void Replace(int Counter) //Method to allow updating of old student marks
        {
            for (int i = 0; i < marks[Counter].Count; i++) //For how many marks exist for that student
            {
                Console.WriteLine("Input a mark (without the percent sign). Input anything else if the course was not taken.");
                string input = Console.ReadLine();
                short mark = MarkProcessor(input);
                marks[Counter][i] = mark; //Replace mark at position with new mark
            }
        }
        void Delete(int Counter) //Method to delete student names
        {
            char input = 'b';
            Console.WriteLine("Please type in '1' to confirm deletion. Note that this is permanent, so please make sure this is what you want."); //Good idea to double check before undertaking destructive action
            try
            {
                input = Convert.ToChar(Console.ReadLine());
            }
            catch (FormatException)
            {
                //do nothing
            }
            if (input == '1')
            {
                names.RemoveAt(Counter); //Remove element at that index
                birthdays.RemoveAt(Counter);
                marks.RemoveAt(Counter);
                phones.RemoveAt(Counter);
                emails.RemoveAt(Counter);
                Console.WriteLine("Deletion complete");
            }
            else
            {
                Console.WriteLine("Deletion cancelled");
            }
        }
        public double Mean(List<short> marks) //Method to calculate average
        {
            double totalAssignments = 0;
            double obtainedMarks = 0;
            double averageMark;
            try
            {
                for (int i = 0; i < marks.Count; i++)
                {
                    if (marks[i] != -1) //If mark is not invalid, use it in average calculation
                    {
                        obtainedMarks += (marks[i] * assignmentWeights[i]); //Multiply assignment by weight
                        totalAssignments += assignmentWeights[i]; //Reflect assignment weighing in divisor
                    }
                }
            }
            catch (ArgumentOutOfRangeException) //It's possible for counter argument in for statement to be out of range when array's empty, although there would be no marks in this case to do anything with
            {
                averageMark = -1;
                return averageMark;
            }
            try
            {
                averageMark = (double)obtainedMarks / totalAssignments; //This is the average of all courses student has taken as a decimal
            }
            catch (DivideByZeroException) //If no assignments in the course
            {
                averageMark = -1;
                return averageMark;
            }
            return averageMark;
        }
        double Median(List<short> marks) //Method to calculate median
        {
            List<short> sorting = new List<short>(); //Use different array to prevent editing order of marks on the first array
            if (marks.Count == 0)
            {
                return -1; //Return invalid number as median doesn't exist with no assignments
            }
            for (int i = 0; i < marks.Count; i++) //For all marks, check if they are valid
            {
                if (marks[i] != -1) //Exclude invalid marks from further sorting
                {
                    sorting.Add(marks[i]);
                }
            }
            sorting.Sort();
            if (sorting.Count % 2 != 0) //https://docs.microsoft.com/en-us/dotnet/api/system.decimal.op_modulus?view=net-5.0 syntax for function to return remainder of division
            {
                return sorting[(int)(sorting.Count / 2.0 - 0.5)]; //Return middle term casted as an int. Use 2.0 instead of 2 to force decimal division
            }
            else //Must be an even number of terms so return mean of middle 2 terms
            {
                int medianPointLower = sorting[sorting.Count / 2 - 1];
                int medianPointHigher = sorting[sorting.Count / 2];
                return (double)(medianPointHigher + medianPointLower) / 2; //Cast to double if median becomes decimal
            }
        }
        int Mode(List<short> marks) //Method to calculate most frequent mark in a set
        {
            int mostFrequent = 0;
            int maximumFrequency = 0;
            if (marks.Count == 0)
            {
                return -1; //Return invalid value as mode doesn't exist with no assignments
            }
            for (int i = 0; i <= 100; i++)
            {
                int numberPresent = 0;
                for (int j = 0; j < marks.Count; j++)
                {
                    if (marks[j] == i) //If mark being counted is equal to mark in array
                    {
                        numberPresent++;
                    }
                }
                if (numberPresent >= maximumFrequency) //This ensures in cases of equal mode, the greatest and most generous to the student is outputted
                {
                    mostFrequent = i; //Set to number being checked
                    maximumFrequency = numberPresent; //Set to amount of instances of said number
                }
            }
            return mostFrequent;
        }
        double ClassMean()
        {
            double obtainedMarks = 0; //Set variables to have a value of zero
            double totalStudents = 0;
            for (int i = 0; i < names.Count; i++)
            {
                double mark = Mean(marks[i]); //Send that student's marks to method for averaginf
                if (mark != -1) //If a valid value
                {
                    obtainedMarks += mark; //Add mark
                    totalStudents++; //Add student to counter
                }
            }
            return (double)(obtainedMarks / totalStudents); //Return value casted as double
        }
        double ClassMedian()
        {
            List<double> sorting = new List<double>();
            for (int i = 0; i < names.Count; i++)
            {
                double mark = Mean(marks[i]); //Get mean mark so median of means can be found
                if (mark != -1) //If a valid value and thus not all "no marks"
                {
                    sorting.Add(mark); //Add mark
                }
            }
            sorting.Sort(); //Using pre-made sorting implementation now that I've discovered it
            if (sorting.Count == 0)
            {
                return -1; //Return invalid mark as no marks to find median of
            }
            if (sorting.Count % 2 != 0) //https://docs.microsoft.com/en-us/dotnet/api/system.decimal.op_modulus?view=net-5.0 syntax for function to return remainder of division
            {
                return sorting[(int)(sorting.Count / 2.0 - 0.5)]; //Return middle term casted as an int. Use 2.0 instead of 2 to force decimal division
            }
            else //Must be an even number of terms so return mean of middle 2 terms
            {
                double medianPointLower = sorting[(sorting.Count / 2 - 1)];
                double medianPointHigher = sorting[(sorting.Count / 2)];
                return (medianPointHigher + medianPointLower) / 2;
            }
        }
        double AssignmentMeans(int assignmentID)
        {
            List<short> assignmentMarks = new List<short>(); //Create a new list to put the assignment marks for further processing into
            for (int i = 0; i < names.Count; i++) //For however many student names there are
            {
                assignmentMarks.Add(marks[i][assignmentID]);
            }
            return Mean(assignmentMarks);
        }
        double AssignmentMedians(int assignmentID)
        {
            List<short> assignmentMarks = new List<short>();
            for (int i = 0; i < names.Count; i++)
            {
                assignmentMarks.Add(marks[i][assignmentID]);
            }
            return Median(assignmentMarks);
        }
        double AssignmentModes(int assignmentID)
        {
            List<short> assignmentMarks = new List<short>();
            for (int i = 0; i < names.Count; i++)
            {
                assignmentMarks.Add(marks[i][assignmentID]);
            }
            return Mode(assignmentMarks);
        }
        int GetAge(int Counter) //Method to calculate age
        {
            int age = DateTime.Now.Year - birthdays[Counter].Year;
            if (DateTime.Now.Month < birthdays[Counter].Month) //For students who haven't had their birthday this year yet
            {
                age--;
            }
            else if (DateTime.Now.Month == birthdays[Counter].Month)
            {
                if (DateTime.Now.Day < birthdays[Counter].Day) //For students who's birthday is later in the month
                {
                    age--;
                }
                if (DateTime.Now.Day == birthdays[Counter].Day) //Birthday celebrator
                {
                    Console.WriteLine("It's {0}'s birthday today!", names[Counter]);
                }
            }
            return age; //Return the age to wherever the method was called
        }
    }
}