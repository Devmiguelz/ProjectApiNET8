namespace PruebaAnnarApi.Application.Response
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T Data { get; }
        public List<string> Errors { get; }

        protected Result(T data, bool isSuccess, List<string> errors)
        {
            if (isSuccess && errors.Count > 0 || !isSuccess && errors.Count == 0)
            {
                throw new ArgumentException("Invalid error state", nameof(errors));
            }

            IsSuccess = isSuccess;
            Data = data;
            Errors = errors;
        }

        public static Result<T> Success(T data) => new(data, true, []);

        public static Result<T> Failure(List<string> errors) => new(default!, false, errors);

        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<List<string>, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Data) : onFailure(Errors);
        }
    }
}
