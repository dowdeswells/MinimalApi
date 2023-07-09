namespace Something.Application.Example.SomeModel;

public class Stay
{
    public int Id { get; set; }
    public DateRange StayDates { get; set; }
    public int KennelId { get; set; }
    public int PetId { get; set; }
    public List<StayAmount> Outgoings { get; set; }
}