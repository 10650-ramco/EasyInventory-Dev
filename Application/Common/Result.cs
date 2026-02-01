namespace Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }

        private Result(bool success, string error)
        {
            IsSuccess = success;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(string error) => new(false, error);
    }
}