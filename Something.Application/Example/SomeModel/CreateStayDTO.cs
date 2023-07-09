namespace Something.Application.Example.SomeModel;

public class CreateStayDTO
{
    public int KennelId { get; set; }
    public int PetId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public IEnumerable<StayAmount> Expenses { get; set; }
}