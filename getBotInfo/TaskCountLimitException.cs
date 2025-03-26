namespace getBotInfo;


    public class TaskCountLimitExceptionException : Exception
    {
        public DateTime ErrorTime { get; }
        public int ErrorCode { get; }
        
        public TaskCountLimitExceptionException()
        {
        }

        public TaskCountLimitExceptionException(string message, int errorCode) : base(message)
        {
            ErrorTime = DateTime.Now;
            ErrorCode = errorCode;
            
        }

        public TaskCountLimitExceptionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
