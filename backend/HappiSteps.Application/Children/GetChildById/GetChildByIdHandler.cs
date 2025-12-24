using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Contracts.Children;

namespace HappiSteps.Application.Children.GetChildById;

public sealed class GetChildByIdHandler
{
    private readonly IChildRepository _repository;

    public GetChildByIdHandler(IChildRepository repository)
    {
        _repository = repository;
    }

    public async Task<ChildResponse?> Handle(GetChildByIdQuery query)
    {
        var child = await _repository.GetByIdAsync(query.ChildId);

        if (child is null)
            return null;

        return new ChildResponse(
            child.ChildId,
            child.OrganisationId,
            child.FirstName,
            child.LastName,
            child.DateOfBirth,
            child.Status.ToString(),
            child.Identifiers.Select(i =>
                new ChildIdentifierResponse(
                    i.Type.ToString(),
                    i.Value,
                    i.AssignedAt
                )
            ).ToList()
        );
    }
}
