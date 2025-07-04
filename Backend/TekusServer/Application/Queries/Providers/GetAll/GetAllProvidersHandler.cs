using MediatR;
using Application.Abstractions.Persistence;
using Application.DTOs;

namespace Application.Queries.Providers.GetAll
{
    internal sealed class GetAllProvidersHandler : IRequestHandler<GetAllProvidersQuery, List<ProviderDto>>
    {
        private readonly IProviderRepository _repository;

        public GetAllProvidersHandler(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProviderDto>> Handle(GetAllProvidersQuery query, CancellationToken cancellationToken)
        {
            var providers = await _repository.SearchAsync(
                query.Search,
                query.SortBy,
                query.Page,
                query.PageSize,
                cancellationToken);

            return providers.Select(p =>
                new ProviderDto(
                    p.Id,
                    p.Nit,
                    p.Name,
                    p.Email.ToString(),
                    p.CustomFields.Select(cf => new CustomFieldDto(cf.Key, cf.Value)).ToList(),
                    p.Services.Select(s =>
                        new ServiceDto(s.Name, s.HourlyRateUsd, s.Countries.ToList())
                    ).ToList()
                )).ToList();
        }
    }
}
