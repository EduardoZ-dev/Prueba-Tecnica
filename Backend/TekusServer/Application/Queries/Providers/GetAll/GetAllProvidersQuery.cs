using Application.DTOs;
using MediatR;

namespace Application.Queries.Providers.GetAll
{
    public sealed record GetAllProvidersQuery(
        string? Search,
        string? SortBy,
        int Page = 1,
        int PageSize = 10
    ) : IRequest<List<ProviderDto>>;
}

