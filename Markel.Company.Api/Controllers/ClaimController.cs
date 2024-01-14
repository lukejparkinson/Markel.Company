using AutoMapper;
using Markel.Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Markel.Company.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly ILogger<ClaimController> _logger;
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;

        public ClaimController(ILogger<ClaimController> logger, IClaimService claimService, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(claimService, nameof(claimService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _logger = logger;
            _claimService = claimService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid ucr)
        {
            var result = await _claimService.Get(ucr);

            return result is not null
                ? Ok(MapToViewModel(result))
                : BadRequest($"Claim with ucr: {ucr} not found");
        }

        [HttpGet("company")]
        public async Task<IActionResult> Get(int companyId)
        {
            var result = await _claimService.GetAllForCompany(companyId);

            return result is not null
                ? Ok(result.Select(MapToViewModel))
                : BadRequest($"Claims for company Id: {companyId} not found");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Domain.ViewModels.CreateClaim claim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Ensure all values are correct");
            }

            var result = await _claimService.Create(MapFromViewModel(claim));

            return result is not null
                ? Ok(MapToViewModel(result))
                : BadRequest("Unable to create claim.");
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid ucr, Domain.ViewModels.UpdateClaim claim)
            => Ok(await _claimService.Update(ucr, MapFromViewModel(claim)));

        private Domain.Dtos.Claim MapFromViewModel(Domain.ViewModels.ClaimBase company)
            => _mapper.Map<Domain.Dtos.Claim>(company);

        private Domain.ViewModels.Claim MapToViewModel(Domain.Dtos.Claim company)
            => _mapper.Map<Domain.ViewModels.Claim>(company);
    }
}
