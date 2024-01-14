using AutoMapper;
using Markel.Company.Repository.Interfaces;
using Markel.Company.Service.Interfaces;

namespace Markel.Company.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(companyRepository, nameof(companyRepository));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<Domain.Dtos.Company?> Get(int id)
        {
            var result = await _companyRepository.Get(id);

            return result is not null
                ? _mapper.Map<Domain.Dtos.Company>(result)
                : null;
        }

        public async Task<Domain.Dtos.Company?> Create(Domain.Dtos.Company company)
        {
            var result = await _companyRepository.Create(_mapper.Map<Domain.Entities.Company>(company));

            return result is not null
                ? _mapper.Map<Domain.Dtos.Company>(result)
                : null;
        }

        public async Task<bool> Update(int id, Domain.Dtos.Company company)
         => await _companyRepository.Update(id, _mapper.Map<Domain.Entities.Company>(company));
    }
}
