// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

string help = "/start\tasks for user name(no numbers)\n" +
              "/help\tdispays user manual\n" +
              "/info\tdisplays program version and release date\n" +
              "/echo\tavaliable only after you entered your name, repeats your input\n" +
              "/exit\texits your programm\n";
string info = "Version 1.0.1 Created 03.03.2025";
string messageCommand = "";
string userName = "";
string pattern = @"^[A-Za-z]+$";
 
GetBotInfo(messageCommand, pattern);
Console.WriteLine("Bye-bye");
 
 
 
 
 
void GetBotInfo(string message, string pattern)
{
    while (message != "/exit")
    {
      
        Console.WriteLine("Hi, " + userName+ "please, enter commands:");
        Console.WriteLine("/start\n/help\n/info\n/echo\n/exit");
        message = Console.ReadLine();
        switch (message)
        {
            case ("/start"):
                if (String.IsNullOrEmpty(userName)) { 
                    Console.WriteLine("Hi, my dear Friend\nWhat is your name?");
                    userName = GetUserName(pattern, userName) + " "; }
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
                if(string.IsNullOrEmpty(userName) || text.Length < 6) {
                    Console.WriteLine("Please, run command /start first or enter text\n");
                    continue;
                }
                Console.WriteLine(text.Substring(6));
                break;
            case ("/exit"):
                break;
            default: Console.WriteLine("I don't understand you, please enter\n"); break;
 
        }
    }
}
 
string GetUserName(string pattern, string userName)
{
    while (!Regex.IsMatch(userName, pattern)) {
        userName = Console.ReadLine();
    }
    return userName.Trim();
}