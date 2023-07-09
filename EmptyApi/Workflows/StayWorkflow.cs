using Something.Application.Example.SomeModel;
using Something.Application.Example.ValidationThoughts;

namespace EmptyApi.Workflows;

public class StayWorkflow
{
    private readonly KennelRepository _kennelRepository;
    private readonly PetRepository _petRepository;
    private readonly StayRepository _stayRepository;

    public StayWorkflow(KennelRepository kennelRepository, PetRepository petRepository, StayRepository stayRepository)
    {
        _kennelRepository = kennelRepository;
        _petRepository = petRepository;
        _stayRepository = stayRepository;
    }
    
    public Result<Stay> Create(CreateStayDTO dto)
    {
        var v = new StayValidator(_kennelRepository, _petRepository);
        var r = v.Validate(dto)
            .Then(stay => _stayRepository.Save(stay));

        return r;
    }
}