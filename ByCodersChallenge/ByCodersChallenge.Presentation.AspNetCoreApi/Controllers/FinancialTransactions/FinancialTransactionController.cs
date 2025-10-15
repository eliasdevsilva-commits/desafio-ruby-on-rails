using BasePoint.Core.Presentation.AspNetCoreApi.Controllers;
using ByCodersChallenge.Core.Application.Dtos.Stores;
using ByCodersChallenge.Core.Application.UseCases.Stores;
using Microsoft.AspNetCore.Mvc;

namespace ByCodersChallenge.Presentation.AspNetCoreApi.Controllers.FinancialTransactions
{
    [Route("api/financial-transactions")]
    [ApiController]
    public class FinancialTransactionController : BaseController
    {
        private readonly ImportFinancialTransactionsUseCase _importFinancialTransactionsUseCase;
        public FinancialTransactionController(
            IHttpContextAccessor httpContextAccessor,
            ImportFinancialTransactionsUseCase importFinancialTransactionsUseCase)
            : base(httpContextAccessor)
        {
            _importFinancialTransactionsUseCase = importFinancialTransactionsUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ImportFinancialTransactionsOutput), StatusCodes.Status201Created)]
        public async Task<IActionResult> ImportFinancialTransactions(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var input = new ImportFinancialTransactionsInput
            {
                MemoryStream = memoryStream
            };

            return OutputConverter(await _importFinancialTransactionsUseCase.ExecuteAsync(input));
        }


    }
}