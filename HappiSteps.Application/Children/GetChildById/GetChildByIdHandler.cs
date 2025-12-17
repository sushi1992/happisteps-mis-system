using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Domain.Children;

namespace HappiSteps.Application.Children.GetChildById;

public sealed class GetChildByIdHandler
{
    private readonly IChildRepository _repository;

    public GetChildByIdHandler(IChildRepository repository)
    {
        _repository = repository;
    }

    public async Task<Child?> Handle(GetChildByIdQuery query)
    {
        return await _repository.GetByIdAsync(query.ChildId);
    }
}
