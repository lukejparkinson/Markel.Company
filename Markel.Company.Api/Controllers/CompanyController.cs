using AutoMapper;
using Markel.Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Markel.Company.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(companyService, nameof(companyService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _logger = logger;
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _companyService.Get(id);

            return result is not null
                ? Ok(MapToViewModel(result))
                : NotFound($"Company with Id: {id} not found");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Domain.ViewModels.CreateCompany company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Ensure all values are correct");
            }

            var result = await _companyService.Create(MapFromViewModel(company));

            return result is not null
                ? Ok(MapToViewModel(result))
                : BadRequest("Unable to create company.");
        }


        [HttpPut]
        public async Task<IActionResult> Put(int id, Domain.ViewModels.UpdateCompany company)
            => Ok(await _companyService.Update(id, MapFromViewModel(company)));

        private Domain.Dtos.Company MapFromViewModel(Domain.ViewModels.CompanyBase company)
            => _mapper.Map<Domain.Dtos.Company>(company);

        private Domain.ViewModels.Company MapToViewModel(Domain.Dtos.Company company)
            => _mapper.Map<Domain.ViewModels.Company>(company);
    }
}
