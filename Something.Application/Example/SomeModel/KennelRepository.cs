

namespace Something.Application.Example.SomeModel;

public class KennelRepository
{
    public Kennel Get(int id)
    {
        if (id == 123)
        {
            return (new Kennel()
            {
                Id = 123,
                Address = "18 Purring Palace Place",
                KennelType = KennelType.Cat,
            });
        }

        if (id == 456)
        {
            return (new Kennel()
            {
                Id = 456,
                Address = "9 Barking Bedsit Street",
                KennelType = KennelType.Dog,
            });
        }

        return null;
    }
}