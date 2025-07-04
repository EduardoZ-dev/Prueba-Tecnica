using MediatR;
using Application.DTOs;

namespace Application.Queries.ServicesProvider.GetById
{
    public sealed record GetServiceByIdQuery(Guid Id) : IRequest<ServiceDto>;
}
