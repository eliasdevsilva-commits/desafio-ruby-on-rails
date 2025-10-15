using BasePoint.Core.Application.Cqrs.QueryProviders;
using BasePoint.Core.Application.Dtos.Input;
using BasePoint.Core.Application.UseCases;
using BasePoint.Core.Presentation.AspNetCoreApi.Controllers;
using ByCodersChallenge.Core.Application.Dtos.FinancialServices;
using ByCodersChallenge.Core.Application.Dtos.FinancialTransactions;
using ByCodersChallenge.Core.Application.UseCases.FinancialTransactions;
using Microsoft.AspNetCore.Mvc;

namespace ByCodersChallenge.Presentation.AspNetCoreApi.Controllers.FinancialTransactions
{
    [Route("api/financial-transactions")]
    [ApiController]
    public class FinancialTransactionController : BaseController
    {
        private readonly ImportFinancialTransactionsUseCase _importFinancialTransactionsUseCase;
        private readonly GetPaginatedResultsUseCase<IListItemOutputCqrsQueryProvider<FinancialTransactionListItemOutput>, FinancialTransactionListItemOutput> _getPaginatedFinancialTransactionUseCase;
        public FinancialTransactionController(
            IHttpContextAccessor httpContextAccessor,
            ImportFinancialTransactionsUseCase importFinancialTransactionsUseCase,
            GetPaginatedResultsUseCase<IListItemOutputCqrsQueryProvider<FinancialTransactionListItemOutput>, FinancialTransactionListItemOutput> getPaginatedFinancialTransactionUseCase)
            : base(httpContextAccessor)
        {
            _importFinancialTransactionsUseCase = importFinancialTransactionsUseCase;
            _getPaginatedFinancialTransactionUseCase = getPaginatedFinancialTransactionUseCase;
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

        [HttpPost("get-by-filter")]
        [ProducesResponseType(typeof(IEnumerable<FinancialTransactionListItemOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginatedFinancialTransaction(GetPaginatedResultsInput input)
        {
            return OutputConverter(await _getPaginatedFinancialTransactionUseCase.ExecuteAsync(input));
        }
    }
}