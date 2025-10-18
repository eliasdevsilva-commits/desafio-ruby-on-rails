using BasePoint.Core.Application.Cqrs.QueryProviders;
using BasePoint.Core.Application.Dtos.Input;
using BasePoint.Core.Application.UseCases;
using BasePoint.Core.Presentation.AspNetCoreApi.Controllers;
using BasePoint.Core.Shared;
using ByCodersChallenge.Core.Application.Dtos.FinancialServices;
using ByCodersChallenge.Core.Application.Dtos.FinancialTransactions;
using ByCodersChallenge.Core.Application.UseCases.FinancialTransactions;
using ByCodersChallenge.Core.Shared;
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
        [ProducesResponseType(typeof(ImportFinancialTransactionsOutput), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportFinancialTransactions(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return OutputConverter(new UseCaseOutput<ImportFinancialTransactionsOutput>(
                    new List<ErrorMessage>() { new(SharedConstants.ErrorMessages.FileIsEmpty) }));

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var input = new ImportFinancialTransactionsInput
            {
                MemoryStream = memoryStream
            };

            // Decided not to pass IFormFile directly to the use case because its from AspNet lib, and the use case should be agnostic from technology 
            var output = await _importFinancialTransactionsUseCase.ExecuteAsync(input);

            return OutputConverter(output);
        }

        [HttpPost("get-by-filter")]
        [ProducesResponseType(typeof(IEnumerable<FinancialTransactionListItemOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<FinancialTransactionListItemOutput>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaginatedFinancialTransaction(GetPaginatedResultsInput input)
        {
            return OutputConverter(await _getPaginatedFinancialTransactionUseCase.ExecuteAsync(input));
        }
    }
}