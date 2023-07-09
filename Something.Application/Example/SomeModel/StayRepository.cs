

using Something.Application.Example.ValidationThoughts;

namespace Something.Application.Example.SomeModel;

public class StayRepository
{
    public Result<Stay> Save(Stay s)
    {
        return Result<Stay>.Success(s);
    }
}