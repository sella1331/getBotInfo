using System.Text.RegularExpressions;
using getBotInfo;

string help = "/start\t\tasks for user name(no numbers)\n" +
               "/help\t\tdispays user manual\n" +
               "/info\t\tdisplays program version and release date\n" +
               "/echo\t\tavaliable only after you entered your name, repeats your input\n" +
               "/addtask\t" + "Adds a task to the list\n" +
               "/showtasks\tshows all tasks\n" +
               "/removetask\t" + "Removes a task from the list\n" +
               "/exit\t\texits your programm\n";
string info = "Version 1.0.1 Created 03.03.2025";
string messageCommand = "";
string userName = "";
string pattern = @"^[A-Za-z]+$";
var taskList = new List<string>();
 
try
{
    GetBotInfo(messageCommand, pattern);
    Console.WriteLine("Bye-bye");
}
catch (TaskLengthLimitException e)
{
    Console.WriteLine("length" + e.Message);
}
catch (TaskCountLimitException e)
{
    Console.WriteLine(e.Message);
}
catch (ArgumentException e)
{
    Console.WriteLine("buba" + e.Message);
}
catch (Exception e)
{
    Console.WriteLine("Произошла непредвиденная ошибка: " + e.GetType() + e.Message + e.StackTrace+  e.InnerException);
}
 
 
 
 
 
void GetBotInfo(string message, string pattern)
{
    int maxNumberTasks = 0;
    int taskLengthLimit = 0;
    GetMaxNumberTasks(ref maxNumberTasks, ref taskLengthLimit);
    GetMaxNumberTasks(ref maxNumberTasks, ref taskLengthLimit);
    //GetMaxLengthString(ref taskLength, taskLengthLimit);
 
    while (message != "/exit")
    {
        Console.WriteLine("Hello, " + userName + "please, enter commands:");
        Console.WriteLine("/start\n/help\n/info\n/echo\n/addtask\n/showtasks\n/removetask\n/exit");
        message = Console.ReadLine();
        ValidateString(message);
        switch (message)
        {
            case ("/start"):
                GetStartMessage(ref userName, pattern);
                break;
            case ("/help"):
                Console.WriteLine(help);
                break;
            case ("/info"):
                Console.WriteLine(info);
                break;
            case string text when text.StartsWith("/echo"):
                GetMessageEcho(text);
                break;
            case ("/addtask"):      
                AddtaskList(maxNumberTasks, taskLengthLimit);
                break;
            case ("/showtasks"):
                ShowTasks();
                break;
            case ("/removetask"):
                RemoveTask();
                break;
            case ("/exit"):
                break;
            default: Console.WriteLine("I don't understand you, please enter\n"); break;
 
        }
    }
}
 
 
void GetMaxNumberTasks(ref int maxNumberTasks, ref int taskLengthLimit)
{
    try
    {
        string inputUser = null;
        if(maxNumberTasks == 0) Console.WriteLine("To start the program enter max number of tasks(1 - 100): ");
        else Console.WriteLine("Введите максимально допустимую длину задачи: ");
        bool numberInRange = int.TryParse(Console.ReadLine(), out int number);
 
        if (!numberInRange || (number < 1 ^ number > 100))
        {
            if (maxNumberTasks == 0) maxNumberTasks = number;
            else taskLengthLimit = number;
            throw new ArgumentException();
        }
        if(maxNumberTasks == 0) maxNumberTasks = number;
        else taskLengthLimit = number;
    }
 
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }
}

void ValidateString(string? str)
{
    try
    {
        if (String.IsNullOrWhiteSpace(str) || String.IsNullOrEmpty(str)) throw new ArgumentNullException(nameof(str));
    }
    catch (ArgumentException e){
        Console.WriteLine(e.Message);
    }  
}
 
void GetStartMessage(ref string userName, string pattern)
{
    if (String.IsNullOrEmpty(userName))
    {
        Console.WriteLine("Hi, my dear Friend\nWhat is your name?");
        userName = GetUserName(pattern, userName) + " ";
 
    }
    else
        Console.WriteLine("You have name, please reboot programm");
}
 
string GetUserName(string pattern, string userName)
{
    while (!Regex.IsMatch(userName, pattern))
    {
        userName = Console.ReadLine();
    }
    return userName.Trim();
}
 
 
void GetMessageEcho(string text)
{
    if (string.IsNullOrEmpty(userName) || text.Length < 6)
    {
        Console.WriteLine("Please, run command /start first or enter text\n");
    }
    else Console.WriteLine(text.Substring(6));
}
 
void AddtaskList(int maxNumberTasks, int taskLengthLimit)
{
    
    Console.WriteLine("Please enter task you would like to add:");
    try
    {
        if (maxNumberTasks > 100) throw new TaskCountLimitException();
        GetInput(taskLengthLimit);
    }
    catch (TaskCountLimitException e)
    {
        Console.WriteLine("Превышено максимальное количество задач равное " + maxNumberTasks);
    }
}
 
void GetInput(int taskLengthLimit)
{
    string input = null;
    int parseToInt = 0;
    int taskLength = 0;
    if ((input = Console.ReadLine()) != null)
    {
        try
        {
            int min = 1;
            int max = 100;
            taskLength = input.Length;
            if (taskLength > taskLengthLimit) throw new TaskLengthLimitException(taskLength, taskLengthLimit);
            if (taskList.Contains(input)) throw new DuplicateTaskException(input);
            parseToInt = ParseAndValidateInt(input, min, max);
 
        }
       
        catch(DuplicateTaskException e)
        {
            Console.WriteLine($"Задача {input} уже существует");
            return;
        }
        catch (TaskLengthLimitException e)
        {
            Console.WriteLine($"Длина задачи {taskLength} превышает максимально допустимое значение {taskLengthLimit}");
            return;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
      
        taskList.Add(input);
        Console.WriteLine("Task added");
    }
    else Console.WriteLine("Please, enter non-empty string");
}
int ParseAndValidateInt(string? str, int min, int max)
{  
    var inputInt = int.TryParse(str, out var value);
    try
    { if (value < min ^ value > max)
            throw new ArgumentException("Not in range of max and min");
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
        return 0;
    }
    return value;
}
 
void ShowTasks()
{
    if (taskList.Count != 0)
    {
        Console.WriteLine("Tasks:");
        foreach (var el in taskList)
        {
            Console.WriteLine(el);
        }
    }
    else Console.WriteLine("No tasks yet");
}
 
void RemoveTask()
{
    if (taskList.Count != 0)
    {
        string numberTask = null;
        int count = 1;
        Console.WriteLine("Please enter the number of the task you would like to remove:");
        foreach (var el in taskList)
        {
            Console.WriteLine(count++ + ". " + el);
        }
        while (String.IsNullOrWhiteSpace(numberTask))
        {
            numberTask = Console.ReadLine();
            if (int.TryParse(numberTask, out int num) && num <= taskList.Count) taskList.RemoveAt(--num);
            else
            {
                Console.WriteLine("Error, repeat enter, please");
                numberTask = String.Empty;
            }
        }
        ;
    }
    else Console.WriteLine("Your tasklist is empty");
}
 