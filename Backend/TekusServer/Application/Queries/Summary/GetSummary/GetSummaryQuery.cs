using Application.Abstractions;
using Application.DTOs;

namespace Application.Queries.Summary.GetSummary
{
    public record GetSummaryQuery() : IQuery<SummaryDto>;
}
