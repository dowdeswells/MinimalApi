namespace Something.Application.Example.ValidationThoughts;

public class ValidationResult<T>
{
    public bool IsValid { get; private set; }
    public T Result { get; private set; }
    public string ErrorMessage { get; private set; }
    public static ValidationResult<T> Ok(T result)
    {
        return new ValidationResult<T>()
        {
            IsValid = true,
            ErrorMessage = null,
            Result = result
        };
    }
}