namespace HRManagementSystem
{
    public class HRSystemException : Exception
    {
        public DateTime Timestamp { get; private set; }

        public HRSystemException() : base()
        {
            Timestamp = DateTime.Now;
        }

        public HRSystemException(string message) : base(message)
        {
            Timestamp = DateTime.Now;
        }

        public HRSystemException(string message, Exception innerException) : base(message, innerException)
        {
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"HRSystemException: {Message}";
        }
    }

}