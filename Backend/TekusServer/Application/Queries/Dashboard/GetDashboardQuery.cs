using Application.Abstractions;
using Application.DTOs;

namespace Application.Queries.Dashboard
{
    public sealed record GetDashboardQuery : IQuery<DashboardDto>;
}
