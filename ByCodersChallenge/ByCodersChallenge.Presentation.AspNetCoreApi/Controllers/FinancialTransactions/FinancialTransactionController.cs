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
        public async Task<IActionResult> ImportFinancialTransactions(IFormFile arquivo)
        {
            using var memoryStream = new MemoryStream();
            await arquivo.CopyToAsync(memoryStream);

            var fileLines = GetStreamLines(memoryStream);

            var input = new ImportFinancialTransactionsInput
            {
                TransactionLines = fileLines
            };

            return OutputConverter(await _importFinancialTransactionsUseCase.ExecuteAsync(input));
        }

        private static List<string> GetStreamLines(MemoryStream memoryStream)
        {
            var stringStream = new StreamReader(memoryStream);

            var lines = new List<string>();

            stringStream.BaseStream.Position = 0;

            while (!stringStream.EndOfStream)
            {
                lines.Add(stringStream.ReadLine());
            }

            return lines;
        }
    }
}