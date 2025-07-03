using Application.Abstractions;
using Application.DTOs;

namespace Application.Queries.Providers.GetById
{
    public sealed record GetProviderByIdQuery(Guid Id) : IQuery<ProviderDto>;
}
