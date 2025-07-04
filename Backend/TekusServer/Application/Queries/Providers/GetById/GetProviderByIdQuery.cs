using Application.DTOs;
using MediatR;

namespace Application.Queries.Providers.GetById
{
    public sealed record GetProviderByIdQuery(Guid Id) : IRequest<ProviderDto>;
}