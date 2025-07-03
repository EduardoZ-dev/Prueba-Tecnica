using Application.Abstractions;
using Application.DTOs;

namespace Application.Queries.ServicesProvider.GetAll
{
    public sealed record GetAllServicesQuery : IQuery<List<ServiceDto>>;
}
