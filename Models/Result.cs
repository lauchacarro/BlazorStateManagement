namespace BlazorStateManagement.Models
{

    public class Result
    {

        public bool Succeeded { get; set; }

        public List<string> Errors { get; set; } = new();

        public static Result Success
            => new Result
            {
                Succeeded = true
            };

        public static Result Failure(IEnumerable<string> errors)
            => new Result
            {
                Succeeded = false,
                Errors = errors.ToList()
            };

        public static implicit operator Result(string error)
            => Failure(new List<string> { error });

        public static implicit operator Result(List<string> errors)
            => Failure(errors.ToList());

        public static implicit operator Result(bool success)
            => success ? Success : Failure(new[] { "Unsuccessful operation." });

        public static implicit operator bool(Result result)
            => result.Succeeded;
    }

    public class Result<TData> : Result
    {

        public TData? Data { get; set; }

        public static Result<TData> SuccessWith(TData data)
            => new Result<TData>()
            {
                Succeeded = true,
                Data = data
            };

        public new static Result<TData> Failure(IEnumerable<string> errors)
            => new Result<TData>()
            {
                Succeeded = false,
                Errors = errors.ToList()
            };

        public static implicit operator Result<TData>(string error)
            => Failure(new List<string> { error });

        public static implicit operator Result<TData>(List<string> errors)
            => Failure(errors);

        public static implicit operator Result<TData>(TData data)
            => SuccessWith(data);

        public static implicit operator bool(Result<TData> result)
            => result.Succeeded;
    }
}
