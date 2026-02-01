namespace Application.DTOs
{
    public sealed class LoginResult
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        private LoginResult(bool success, string? error)
        {
            IsSuccess = success;
            Error = error;
        }

        public static LoginResult Success()
            => new(true, null);

        public static LoginResult Failure(string error)
            => new(false, error);
    }
}