namespace Something.Application.Example.SomeModel;

public class PetRepository
{
    public const string PetNotFound = "Pet not found";
    public Pet Get(int id)
    {
        if (id == 789)
        {
            return (new Pet()
            {
                Id = 789,
                Name = "Bonzo",
            });
        }

        if (id == 666)
        {
            return (new Pet()
            {
                Id = 666,
                Name = "Flash",
            });
        }

        return null;
    }

}