using MediatR;
using Application.DTOs;

namespace Application.Queries.ServicesProvider.GetAll
{
    public sealed record GetAllServicesQuery : IRequest<List<ServiceDto>>;
}
