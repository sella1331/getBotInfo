namespace getBotInfo;


public class TaskCountLimitException : Exception
{
      
    public int maxNunmber { get; }
     
    
 
    public TaskCountLimitException()
    {
    }
 
    public TaskCountLimitException(string message, int MaxNumber) : base(message)
    {
        maxNunmber = MaxNumber;
    }
}
 
 
public class TaskLengthLimitException : Exception
{
    public int taskLength { get; }
    public int taskLengthLimit { get; }
 
    public TaskLengthLimitException() { }
 
    public TaskLengthLimitException( int TaskLength, int TaskLengthLimit)
    { taskLength = TaskLength;
        taskLengthLimit = TaskLengthLimit;
    }
 
}
 
 
  
public class DuplicateTaskException : Exception
{
    public string task { get; }
    
 
    public DuplicateTaskException() { }
 
    public DuplicateTaskException(string Task)
    {
        task = Task;       
    }
 
}
