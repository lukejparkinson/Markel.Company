using AutoMapper;
using Markel.Company.Domain.Dtos;
using Markel.Company.Repository.Interfaces;
using Markel.Company.Service.Interfaces;

namespace Markel.Company.Service
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;

        public ClaimService(IClaimRepository claimRepository, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(claimRepository, nameof(claimRepository));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        public async Task<Claim?> Get(Guid ucr)
        {
            var result = await _claimRepository.Get(ucr);

            return result is not null
                ? MapFromEntity(result)
                : null;
        }

        public async Task<IEnumerable<Claim>> GetAllForCompany(int companyId)
        {
            var result = await _claimRepository.GetAllForCompany(companyId);

            return result.Select(s => MapFromEntity(s));
        }

        public async Task<Claim?> Create(Claim claim)
        {
            var result = await _claimRepository.Create(MapToEntity(claim));

            return result is not null
                ? MapFromEntity(result)
                : null;
        }

        public Task<bool> Update(Guid ucr, Claim claim)
            => _claimRepository.Update(ucr, MapToEntity(claim));

        private Claim MapFromEntity(Domain.Entities.Claim claim)
            => _mapper.Map<Claim>(claim);

        private Domain.Entities.Claim MapToEntity(Claim claim)
            => _mapper.Map<Domain.Entities.Claim>(claim);
    }
}
