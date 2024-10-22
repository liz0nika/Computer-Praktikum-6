using System;
using System.Collections;
using System.Text.RegularExpressions;
namespace KP6
{
    public abstract class Program
    {
        static List<Student> students = new List<Student>();
        static int nextId = 1;

        static int EnterSize()
        {
            Console.WriteLine("Enter size:");
            int n = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            return n;
        }
        
        static int[,] RandomArray()
        {
            var n = EnterSize();
            Random random = new Random();
            var array = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = random.Next(10);
                }
            }
            return array;
        }

        static int[,] OneArray()
        {
            var n = EnterSize();
            var array = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = 1;
                }
            }
            return array;
        }

        static int[,] FillMatrixWithDiamond()
        {
            var n = EnterSize();
            var array = OneArray(); // матриця з одиницями
            var center = n / 2;

            for (int i = 0; i <= center; i++)
            {
                for (int j = center - i; j <= center + i; j++)
                {
                    array[i, j] = 0; // верхня половина ромба
                    array[n - i - 1, j] = 0; // нижня половина ромба
                }
            }
            return array;
        }

        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        
        public class Student
        {
            public int Id { get; private set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            
            private static int nextId = 1;
            public Student(string name, string surname, int age)
            {
                Id = nextId++;
                Name = name;
                Surname = surname;
                Age = age;
            }
            static List<Student> students = new List<Student>();

            //public override string ToString()
            //{
            //    return $"ID: {Id}, Surname: {Surname}, Name: {Name}, Age: {Age}";
            //}
        }
        
        static bool CheckStudentName(string name)
        {
            var patternName = "^[A-Za-z]+([-' ][A-Za-z]+)*$";
            Regex regex = new Regex(patternName);
            foreach (var c in name)
            {
                if (!Char.IsLetter(c) && c != '-' && c != '`')
                {
                    return false;
                }
            }
            return regex.IsMatch(name);
        }

        static bool CheckStudentAge(int age)
        {
            if (age > 0 && age < 120) // допустимий діапазон віку
            {
                return true;
            }
            else
            {
                Console.WriteLine("Age should be between 1 and 119.");
                return false;
            }
        }

        
        static void AddStudent()
        {
            Console.WriteLine("Enter surname: ");
            string surname = Console.ReadLine();
            if (!CheckStudentName(surname))
            {
                Console.WriteLine("Invalid surname. Only letters, hyphens, and apostrophes are allowed.");
                return;
            }
            
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();
            if (!CheckStudentName(name))
            {
                Console.WriteLine("Invalid name. Only letters, hyphens, and apostrophes are allowed.");
                return;
            }
            Console.WriteLine("Enter age: ");
            int age = int.Parse(Console.ReadLine());
            if (CheckStudentAge(age))
            {
                Student newStudent = new Student(name, surname, age);

                students.Add(newStudent);
                Console.WriteLine("Student added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid age. Student not added.");
            }
        }
        
        static void ClearStudents()
        {
            students.Clear();
            Console.WriteLine("Student collection cleared.");
        }
        
        static void DeleteStudent()
        {
            Console.WriteLine("Enter student ID to delete:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Student student = students.FirstOrDefault(s => s.Id == id);
                if (student != null)
                {
                    students.Remove(student);
                    Console.WriteLine("Student deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void SearchStudents()
        {
            Console.WriteLine("Enter student's name or surname to search:");
            string search = Console.ReadLine().ToLower();
            var results = students.Where(s => s.Name.ToLower().Contains(search) 
                                              || s.Surname.ToLower().Contains(search)).ToList();

            if (results.Count > 0)
            {
                Console.WriteLine("Students found:");
                foreach (var s in results)
                {
                    Console.WriteLine($"ID: {s.Id}, Name: {s.Name}, Surname: {s.Surname}, Age: {s.Age}");
                }
            }
            else
            {
                Console.WriteLine("No students found.");
            }
        }


        static void SortStudents(bool ascending)
        {
            if (ascending)
            {
                Console.WriteLine("Students sorted in ascending order by surname.");
                students = students.OrderBy(s => s.Surname).ToList();
            }
            else
            {
                Console.WriteLine("Students sorted in descending order by surname.");
                students = students.OrderByDescending(s => s.Surname).ToList();
            }

            // Print the sorted list
            PrintStudents();
        }


        static void PrintStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students in the collection.");
                return;
            }

            Console.WriteLine("Student Collection:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Age: {student.Age}");
            }
        }


        static ArrayList MakeArrayList()
        {
            int n = EnterSize();
            ArrayList arrayList = new ArrayList(n);
            return arrayList;
        }

        static ArrayList FillArrayList(ArrayList arrayList)
        {
            for (int i = 0; i < arrayList.Capacity; i++)
            {
                Console.WriteLine($"Enter element {i + 1}:");
                int newElement = int.Parse(Console.ReadLine());
                arrayList.Add(newElement);
                Console.WriteLine($"Element {newElement} added to the ArrayList.");
            }
            return arrayList;
        }
        static ArrayList RandomArrayList(ArrayList arrayList)
        {
            Random random = new Random();
            for (int i = 0; i < arrayList.Capacity; i++)
            {
                arrayList.Add(random.Next(10)); // Генерация чисел от 0 до 10
            }
            return arrayList;
        }

        static void AddElemetsArrayList(ArrayList arrayList)
        {
            Console.WriteLine("How many elements would you like to add?");
            int numElements = int.Parse(Console.ReadLine());

            for (int i = 0; i < numElements; i++)
            {
                Console.WriteLine($"Enter element {i + 1}:");
                int newElement = int.Parse(Console.ReadLine());
                arrayList.Add(newElement);
                Console.WriteLine($"Element {newElement} added to the ArrayList.");
            }
        }

        static void PrintArrayList(ArrayList arrayList)
        {
            Console.WriteLine("Array List:");
            foreach (var element in arrayList)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }



        static SortedList MakeSortedArrayList()
        {
            SortedList sortedlist = new SortedList();
            
            return sortedlist;
        }

        

        static SortedList FillSortedList(SortedList sortedList)
        {
            int n = EnterSize();

            for (int i = 0; i < n; i++)
            {
                int key = 0;  // Initialize key
                bool isUniqueKey = false;

                // Loop until a unique key is entered
                while (!isUniqueKey)
                {
                    Console.WriteLine($"Enter key {i + 1}:");
                    key = int.Parse(Console.ReadLine());

                    // Check if the key already exists in the SortedList
                    if (sortedList.ContainsKey(key))
                    {
                        Console.WriteLine($"Key {key} already exists. Please enter a unique key.");
                    }
                    else
                    {
                        isUniqueKey = true; // The key is unique, exit the loop
                    }
                }

                Console.WriteLine($"Enter value {i + 1}:");
                int value = int.Parse(Console.ReadLine());
                sortedList.Add(key, value);
                Console.WriteLine($"Key {key} with Value {value} added to the SortedList.");
            }

            return sortedList;
        }


        
        static SortedList RandomSortedList(SortedList sortedList)
        {
            int n = EnterSize();
            Random random = new Random();
    
            for (int i = 0; i < n; i++) // Generate random elements
            {
                int key;
                int attempts = 0;
                const int maxAttempts = 100; // Set a limit for attempts to avoid infinite loops
        
                do
                {
                    key = random.Next(1000); // Generate a random key between 0 and 999
                    attempts++;
                } while (sortedList.ContainsKey(key) && attempts < maxAttempts); // Ensure the key is unique

                // If the maximum attempts were reached, notify and skip adding this entry
                if (attempts >= maxAttempts)
                {
                    Console.WriteLine("Max attempts reached for generating a unique key. Skipping this entry.");
                    continue; // Skip this iteration if a unique key couldn't be found
                }

                int value = random.Next(100); // Generate a random value between 0 and 99
                sortedList.Add(key, value); // Add the key-value pair to the SortedList
            }

            return sortedList;
        }


        static void AddElementsSortedList(SortedList sortedList)
        {
            Console.WriteLine("How many elements would you like to add?");
            int numElements = int.Parse(Console.ReadLine());

            for (int i = 0; i < numElements; i++)
            {
                int key = 0;  // Initialize key
                bool isUniqueKey = false;

                // Loop until a unique key is entered
                while (!isUniqueKey)
                {
                    Console.WriteLine($"Enter key {i + 1}:");
                    key = int.Parse(Console.ReadLine());

                    // Check if the key already exists in the SortedList
                    if (sortedList.ContainsKey(key))
                    {
                        Console.WriteLine($"Key {key} already exists. Please enter a unique key.");
                    }
                    else
                    {
                        isUniqueKey = true; // The key is unique, exit the loop
                    }
                }

                Console.WriteLine($"Enter value {i + 1}:");
                int value = int.Parse(Console.ReadLine());
                sortedList.Add(key, value);
                Console.WriteLine($"Key {key} with Value {value} added to the SortedList.");
            }
        }
        
        static void PrintSortedList(SortedList sortedList)
        {
            Console.WriteLine("SortedList contents:");
            foreach (DictionaryEntry entry in sortedList)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }
        }

        static Stack<int> MakeStackInt()
        {
            Stack<int> stack_int = new Stack<int>();
            return stack_int;
        }

        static Stack<int> FillStackInt(Stack<int> stack_int)
        {
            int n = EnterSize();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter element {i + 1}:");
                int newElement = int.Parse(Console.ReadLine());
                stack_int.Push(newElement);
                Console.WriteLine($"Element {newElement} added to the ArrayList.");
            }
            return stack_int;
        }

        static Stack<int> RandomStackInt(Stack<int> stack_int)
        {
            int n = EnterSize();
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                stack_int.Push(random.Next(10)); // Генерация чисел от 0 до 10
            }
            return stack_int;
        }

        static Stack<int> AddElementsStack_int(Stack<int> stack_int)
        {
            Console.WriteLine("How many elements would you like to add?");
            int numElements = int.Parse(Console.ReadLine());

            for (int i = 0; i < numElements; i++)
            {
                Console.WriteLine($"Enter element {i + 1}:");
                int newElement = int.Parse(Console.ReadLine());
                stack_int.Push(newElement);
                Console.WriteLine($"Element {newElement} added to the ArrayList.");
            }
            return stack_int;
        }
        
        
        static void PrintStackInt(Stack<int> stack_int)
        {
            Console.WriteLine("Stack of int:");
            foreach (var element in stack_int)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }
        
        static Stack<string> MakeStackString()
        {
            Stack<string> stack_string = new Stack<string>();
            return stack_string;
        }

        static Stack<string> FillStackString(Stack<string> stack_string)
        {
            int n = EnterSize();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter element {i + 1}:");
                string newString = Console.ReadLine();
                stack_string.Push(newString);
                Console.WriteLine($"String {newString} added to the ArrayList.");
            }
            return stack_string;
        }
        
        static Stack<string> AddElementsStack_string(Stack<string> stack_string)
        {
            Console.WriteLine("How many strings would you like to add?");
            int numElements = int.Parse(Console.ReadLine());
            for (int i = 0; i < numElements; i++)
            {
                Console.WriteLine($"Enter string {i + 1}:");
                string newElement = Console.ReadLine();
                stack_string.Push(newElement);
                Console.WriteLine($"String {newElement} added to the ArrayList.");
            }
            return stack_string;
        }

        static void PrintStackString(Stack<string> stack_string)
        {
            Console.WriteLine("Stack of strings:");
            foreach (var element in stack_string)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }
        
        static Dictionary<int, string> MakeDictionary()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            return dictionary;
        }

        static Dictionary<int, string> FillDictionary(Dictionary<int, string> dictionary)
        {
            int n = EnterSize();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter key {i + 1}:");
                int key = int.Parse(Console.ReadLine());
                Console.WriteLine($"Enter value {i + 1}:");
                string value = Console.ReadLine();
                dictionary.Add(key, value);
                Console.WriteLine($"Key {key} with Value {value} added to the Dictionary.");
            }
            return dictionary;
        }

        static void AddElementsDictionary(Dictionary<int, string> dictionary)
        {
            Console.WriteLine("How many elements would you like to add?");
            int numElements = int.Parse(Console.ReadLine());

            for (int i = 0; i < numElements; i++)
            {
                Console.WriteLine($"Enter key for element {i + 1}:");
                int key = int.Parse(Console.ReadLine());
                Console.WriteLine($"Enter value for element {i + 1}:");
                string value = Console.ReadLine();
                dictionary.Add(key, value);
                Console.WriteLine($"Key {key} with Value {value} added to the SortedList.");
            }
        }
        
        static void PrintDictionary(Dictionary<int, string> dictionary)
        {
            Console.WriteLine("Dictionary contents:");
            foreach (KeyValuePair<int, string> entry in dictionary)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }
        }
        static void TwoDimensionalArrayMenu()
    {
        bool arrayMenu = true;

        while (arrayMenu)
        {
            Console.WriteLine("\n1. To make random array." +
                              "\n2. To make array with '1'." +
                              "\n3. To make array with rhombus of '0'." +
                              "\n4. Back to main menu.");

            int choice_2d_array = int.Parse(Console.ReadLine());

            switch (choice_2d_array)
            {
                case 1:
                    int[,] rand_array = RandomArray();
                    ShowArrayMenu(rand_array);
                    break;
                case 2:
                    int[,] one_array = OneArray();
                    ShowArrayMenu(one_array);
                    break;
                case 3:
                    int[,] diamond_matrix = FillMatrixWithDiamond();
                    ShowArrayMenu(diamond_matrix);
                    break;
                case 4:
                    arrayMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ShowArrayMenu(int[,] array)
    {
        bool showArrayMenu = true;

        while (showArrayMenu)
        {
            Console.WriteLine("\n1. Do you want to see array?" +
                              "\n2. Back to previous menu.");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    PrintMatrix(array);
                    break;
                case 2:
                    showArrayMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void StudentsCollectionMenu()
    {
        bool studentsMenu = true;

        while (studentsMenu)
        {
            Console.WriteLine("\n1. To add information to student's collection." +
                              "\n2. Delete existing record." +
                              "\n3. Search existing record." +
                              "\n4. Sort existing record (ascending)." +
                              "\n5. Sort existing record (descending)." +
                              "\n6. Clear collection." +
                              "\n7. Show student's collection." +
                              "\n8. Back to main menu.");

            int choice_students = int.Parse(Console.ReadLine());

            switch (choice_students)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    DeleteStudent();
                    break;
                case 3:
                    SearchStudents();
                    break;
                case 4:
                    SortStudents(true);
                    break;
                case 5:
                    SortStudents(false);
                    break;
                case 6:
                    ClearStudents();
                    break;
                case 7:
                    PrintStudents();
                    break;
                case 8:
                    studentsMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CollectionsMenu()
    {
        bool collectionsMenu = true;

        while (collectionsMenu)
        {
            Console.WriteLine("1. Make and fill ArrayList." +
                              "\n2. Make and fill SortedList" +
                              "\n3. Make and fill Stack." +
                              "\n4. Make and fill Dictionary" +
                              "\n5. Back to main menu.");

            int choice_collections = int.Parse(Console.ReadLine());

            switch (choice_collections)
            {
                case 1:
                    ArrayListMenu();
                    break;
                case 2:
                    SortedListMenu();
                    break;
                case 3:
                    StackMenu();
                    break;
                case 4:
                    DictionaryMenu();
                    break;
                case 5:
                    collectionsMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ArrayListMenu()
    {
        bool arrayListMenu = true;

        while (arrayListMenu)
        {
            Console.WriteLine("1. Fill Int ArrayList." +
                              "\n2. Random Int fill ArrayList." +
                              "\n3. Back to previous menu.");

            int choice_fill_arraylist = int.Parse(Console.ReadLine());

            switch (choice_fill_arraylist)
            {
                case 1:
                    ArrayList arrayl = MakeArrayList();
                    ArrayList arraylist = FillArrayList(arrayl);
                    ArrayListOperationsMenu(arraylist);
                    break;

                case 2:
                    ArrayList arrayr = MakeArrayList();
                    ArrayList arrayrandom = RandomArrayList(arrayr);
                    ArrayListOperationsMenu(arrayrandom);
                    break;
                case 3:
                    arrayListMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ArrayListOperationsMenu(ArrayList arrayList)
    {
        bool arrayListOperationsMenu = true;

        while (arrayListOperationsMenu)
        {
            Console.WriteLine("1. Add more ints to ArrayList." +
                              "\n2. See ArrayList." +
                              "\n3. Back to previous menu.");

            int choice_do_arraylist = int.Parse(Console.ReadLine());

            switch (choice_do_arraylist)
            {
                case 1:
                    AddElemetsArrayList(arrayList);
                    break;
                case 2:
                    PrintArrayList(arrayList);
                    break;
                case 3:
                    arrayListOperationsMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void SortedListMenu()
    {
        bool sortedListMenu = true;

        while (sortedListMenu)
        {
            Console.WriteLine("1. Fill SortedList." +
                              "\n2. Random fill SortedList." +
                              "\n3. Back to previous menu.");

            int choice_fill_sortedlist = int.Parse(Console.ReadLine());

            switch (choice_fill_sortedlist)
            {
                case 1:
                    SortedList sortedList = MakeSortedArrayList();
                    FillSortedList(sortedList);
                    SortedListOperationsMenu(sortedList);
                    break;

                case 2:
                    SortedList randomSortedList = MakeSortedArrayList();
                    RandomSortedList(randomSortedList);
                    SortedListOperationsMenu(randomSortedList);
                    break;

                case 3:
                    sortedListMenu = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void SortedListOperationsMenu(SortedList sortedList)
    {
        bool sortedListOperationsMenu = true;

        while (sortedListOperationsMenu)
        {
            Console.WriteLine("1. Add elements to SortedList." +
                              "\n2. See SortedList." +
                              "\n3. Back to previous menu.");

            int choice_do_sortedlist = int.Parse(Console.ReadLine());

            switch (choice_do_sortedlist)
            {
                case 1:
                    AddElementsSortedList(sortedList);
                    break;
                case 2:
                    PrintSortedList(sortedList);
                    break;
                case 3:
                    sortedListOperationsMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void StackMenu()
    {
        bool stackMenu = true;

        while (stackMenu)
        {
            Console.WriteLine("1. Make and fill Stack of ints." +
                              "\n2. Make and fill Stack of strings." +
                              "\n3. Back to previous menu.");

            int choice_fill_stack = int.Parse(Console.ReadLine());

            switch (choice_fill_stack)
            {
                case 1:
                    Stack<int> stack_int = MakeStackInt();
                    Console.WriteLine("1. Fill Stack of ints." +
                                      "\n2. Random fill Stack of ints." +
                                      "\n3. Back to previous menu.");
                    int choice_fill_stack_int = int.Parse(Console.ReadLine());
                    switch (choice_fill_stack_int)
                    {
                        case 1:
                            FillStackInt(stack_int);
                            break;
                        case 2:
                            RandomStackInt(stack_int);
                            break;
                        case 3:
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    StackOperationsMenu(stack_int);
                    break;

                case 2:
                    Stack<string> stack_string = MakeStackString();
                    FillStackString(stack_string);
                    StackOperationsMenu(stack_string);
                    break;

                case 3:
                    stackMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void StackOperationsMenu<T>(Stack<T> stack)
    {
        bool stackOperationsMenu = true;

        while (stackOperationsMenu)
        {
            Console.WriteLine("1. Add elements to Stack." +
                              "\n2. See Stack." +
                              "\n3. Back to previous menu.");

            int choice_do_stack = int.Parse(Console.ReadLine());

            switch (choice_do_stack)
            {
                case 1:
                    if (typeof(T) == typeof(int))
                        AddElementsStack_int(stack as Stack<int>);
                    else if (typeof(T) == typeof(string))
                        AddElementsStack_string(stack as Stack<string>);
                    else
                        Console.WriteLine("Unsupported stack type.");
                    break;
                case 2:
                    if (typeof(T) == typeof(int))
                        PrintStackInt(stack as Stack<int>);
                    else if (typeof(T) == typeof(string))
                        PrintStackString(stack as Stack<string>);
                    else
                        Console.WriteLine("Unsupported stack type.");
                    break;
                case 3:
                    stackOperationsMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }


    static void DictionaryMenu()
    {
        bool dictionaryMenu = true;

        while (dictionaryMenu)
        {
            Console.WriteLine("1. Add elements to Dictionary." +
                              "\n2. See Dictionary." +
                              "\n3. Back to previous menu.");

            Dictionary<int, string> dictionary = MakeDictionary();
            FillDictionary(dictionary);

            int choice_do_dictionary = int.Parse(Console.ReadLine());

            switch (choice_do_dictionary)
            {
                case 1:
                    AddElementsDictionary(dictionary);
                    break;
                case 2:
                    PrintDictionary(dictionary);
                    break;
                case 3:
                    dictionaryMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

   

        
        static void Main(string[] args)
        {
            bool mainMenu = true;

            while (mainMenu)
            {
                Console.WriteLine("Choose an option:"
                                  + "\n 1. Operations with two-dimensional array."
                                  + "\n 2. Operations with student's collection."
                                  + "\n 3. Operations with different types of collections. " +
                                  "\n(ArrayList, SortedList, Stack, Dictionary). ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        TwoDimensionalArrayMenu();
                        break;

                    case 2:
                        StudentsCollectionMenu();
                        break;

                    case 3:
                        CollectionsMenu();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
