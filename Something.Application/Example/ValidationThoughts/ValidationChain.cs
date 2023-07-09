namespace Something.Application.Example.ValidationThoughts;

public class ValidationChain<T>
{
    private bool _isValid = true;
    private string _message = "";
    private List<Func<(bool, string)>> _validationFuncs = new List<Func<(bool, string)>>();
    private Func<T> _fin;
    
    public class Result
    {
        public T Answer { get; private set; }
        public bool IsValid { get; private set; }
        public string Message { get; private set; }

        public static Result Ok(T answer)
        {
            return new Result()
            {
                Answer = answer,
                IsValid = true,
                Message = ""
            };
        }

        public static Result Error(string message)
        {
            return new Result()
            {
                Message = message,
                IsValid = false,
                Answer = default
            };
        }
    }
    
    public ValidationChain<T> Return(Func<T> fin)
    {
        _fin = fin;
        return this;
    }
    
    public ValidationChain<T> Chain(Func<(bool, string)> validator)
    {
        _validationFuncs.Add(validator);
        return this;
    }
    
    public Result Execute()
    {
        _validationFuncs.ForEach(
            f =>
            {
                if (_isValid)
                {
                    (_isValid, _message) = f();
                }
            });
        
        if (_isValid)
        {
            return Result.Ok(_fin());
        }

        return Result.Error(_message);
    }
}