using Something.Application.Example.SomeModel;

namespace Something.Application.Example.ValidationThoughts;

public class StayValidator
{
    public const string OutgoingsAreMandatoryForADogKennel = "Outgoings are not mandatory for a dog kennel";
    public const string OutgoingsAreNotApplicableToACatKennel = "Outgoings are not applicable to a cat kennel";
    public const string EndDateMustBeOnOrAfterStartDate = "End date must be on or after start date";
    public const string KennelNotFound = "Kennel not found";
    public const string PetNotFound = "Pet not found";
    
    private readonly KennelRepository _repository;
    private readonly PetRepository _petRepository;
    
    public StayValidator(KennelRepository repository, PetRepository petRepository)
    {
        _repository = repository;
        _petRepository = petRepository;
    }

    public Result<Stay> Validate(CreateStayDTO planDto)
    {
        var r = ValidateKennel(planDto.KennelId)
            .Then(kennel => ValidatePet(planDto.PetId)
                .Then(pet => ValidateOutgoings(kennel, planDto.Expenses)
                    .Then(stayAmounts => ValidateDateRange(planDto.StartDate, planDto.EndDate)
                        .Then(dates =>
                        {
                            var m = new Stay()
                            {
                                KennelId = kennel.Id,
                                PetId = pet.Id,
                                StayDates = dates,
                                Outgoings = stayAmounts
                            };
                            return Result<Stay>.Success(m);
                        })
                    )));
        return r;
    }

    private Result<Kennel> ValidateKennel(int id)
    {
        return ToResult(_repository.Get, id, KennelNotFound);
    }

    private Result<Pet> ValidatePet(int id)
    {
        return ToResult(_petRepository.Get, id, PetNotFound);
    }

    private Result<T> ToResult<T>(Func<int, T> get, int id, string error)
    {
        var item = get(id);
        if (item == null)
        {
            return Result<T>.Failure(error);
        }

        return Result<T>.Success(item);
    }

    private Result<List<StayAmount>> ValidateOutgoings(Kennel p, IEnumerable<StayAmount> outgoings)
    {
        if (p.KennelType == KennelType.Dog)
        {
            if (outgoings == null || !outgoings.Any())
            {
                return Result<List<StayAmount>>.Failure(OutgoingsAreMandatoryForADogKennel);
            }
            return Result<List<StayAmount>>.Success(outgoings.ToList());
        }
        
        if (p.KennelType == KennelType.Cat)
        {
            if (outgoings != null && outgoings.Any())
            {
                return Result<List<StayAmount>>.Failure(OutgoingsAreNotApplicableToACatKennel);
            }
        }
        
        return Result<List<StayAmount>>.Success(new List<StayAmount>());

    }

    private Result<DateRange> ValidateDateRange(DateTime start, DateTime? end)
    {
        if (end.HasValue && end.Value < start)
        {
            return Result<DateRange>.Failure(EndDateMustBeOnOrAfterStartDate);
        }

        return Result<DateRange>.Success(new DateRange(ToDateOnly(start), end.HasValue ? ToDateOnly(end.Value) : null));
    }

    private DateOnly ToDateOnly(DateTime d)
    {
        return new DateOnly(d.Year, d.Month, d.Day);
    }
}