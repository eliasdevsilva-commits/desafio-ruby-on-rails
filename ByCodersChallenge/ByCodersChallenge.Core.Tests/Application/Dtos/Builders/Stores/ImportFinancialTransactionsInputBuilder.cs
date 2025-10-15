using ByCodersChallenge.Core.Application.Dtos.Stores;

namespace ByCodersChallenge.Core.Tests.Application.Dtos.Builders.Stores
{
    public class ImportFinancialTransactionsInputBuilder
    {
        private MemoryStream _memoryStream;

        public ImportFinancialTransactionsInputBuilder()
        {
            _memoryStream = new MemoryStream();

            _memoryStream.WriteByte(1);
        }
        public ImportFinancialTransactionsInputBuilder WithMemoryStream(MemoryStream memoryStream)
        {
            _memoryStream = memoryStream;
            return this;
        }

        public ImportFinancialTransactionsInput Build()
        {
            return new ImportFinancialTransactionsInput
            {
                MemoryStream = _memoryStream
            };
        }
    }
}