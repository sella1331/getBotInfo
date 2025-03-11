// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

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
 
GetBotInfo(messageCommand, pattern);
Console.WriteLine("Bye-bye");
 


void GetBotInfo(string message, string pattern)
{
    while (message != "/exit")
    {
 
        Console.WriteLine("Привет, " + userName + "please, enter commands:");
        Console.WriteLine("/start\n/help\n/info\n/echo\n/addtask\n/showtasks\n/removetask\n/exit");
        message = Console.ReadLine();
       switch (message)
        {
            case ("/start"):
                if (String.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("Hi, my dear Friend\nWhat is your name?");
                    userName = GetUserName(pattern, userName) + " ";
                }
                else
                    Console.WriteLine("You have name, please reboot programm");
 
                break;
            case ("/help"):
                Console.WriteLine(help);
                break;
            case ("/info"):
                Console.WriteLine(info);
                break;
            case string text when text.StartsWith("/echo"):
                if (string.IsNullOrEmpty(userName) || text.Length < 6)
                {
                    Console.WriteLine("Please, run command /start first or enter text\n");
                    continue;
                }
                Console.WriteLine(text.Substring(6));
                break;
            case ("/addtask"):
                AddtaskList();
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
 
string GetUserName(string pattern, string userName)
{
    while (!Regex.IsMatch(userName, pattern))
    {
        userName = Console.ReadLine();
    }
    return userName.Trim();
}
 
void AddtaskList()
{
    string input;
    Console.WriteLine("Please enter task you would like to add:");
    if ((input = Console.ReadLine()) != null)
    {
        taskList.Add(input);
        Console.WriteLine("Task added");
    }
    else Console.WriteLine("Please, enter non-empty string");
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
 
 