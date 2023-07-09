namespace Something.Application.Example.ValidationThoughts;

public class Result<T>
{
    public static Result<T> Success(T v)
    {
        return new Result<T>
        {
            Value = v,
            IsValid = true,
            Error = "",
        };
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>
        {
            Value = default,
            IsValid = false,
            Error = error,
        };
    }

    public Result<Tb> Then<Tb>(Func<T, Result<Tb>> f)
    {
        return IsValid ? f(Value) : CastAs<Tb>();
    }

    public Result<Tb> CastAs<Tb>()
    {
        return Result<Tb>.Failure(Error);
    }

    public T Value { get; set; }
    public bool IsValid { get; set; }
    public string Error { get; set; }
}