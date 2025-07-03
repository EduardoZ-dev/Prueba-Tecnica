using Application.Abstractions;
using Application.DTOs;

namespace Application.Queries.ServicesProvider.GetById
{
    public sealed record GetServiceByIdQuery(Guid Id) : IQuery<ServiceDto>;
}
