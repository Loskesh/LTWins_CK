namespace HRManagementSystem
{
    public class ValidationException : HRSystemException
    {
        public List<string> ValidationErrors { get; private set; }

        public ValidationException() : base()
        {
            ValidationErrors = new List<string>();
        }

        public ValidationException(string message) : base(message)
        {
            ValidationErrors = new List<string> { message };
        }

        public ValidationException(List<string> errors) : base("Validation failed. See ValidationErrors for details.")
        {
            ValidationErrors = errors ?? new List<string>();
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            ValidationErrors = new List<string> { message };
        }
    }

}