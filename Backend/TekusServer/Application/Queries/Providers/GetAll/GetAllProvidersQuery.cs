using Application.Abstractions;
using Application.DTOs;

namespace Application.Queries.Providers.GetAll
{
    public sealed record GetAllProvidersQuery(
        string? Search,
        string? SortBy,
        int Page = 1,
        int PageSize = 10
    ) : IQuery<List<ProviderDto>>;
}
