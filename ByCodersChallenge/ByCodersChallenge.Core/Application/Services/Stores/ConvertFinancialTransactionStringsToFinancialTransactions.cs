using ByCodersChallenge.Core.Application.Services.Stores.Interfaces;
using ByCodersChallenge.Core.Domain.Entities;

namespace ByCodersChallenge.Core.Application.Services.Stores
{
    public class ConvertFinancialTransactionStringsToFinancialTransactions : IConvertFinancialTransactionStringsToFinancialTransactions
    {
        public List<FinancialTransaction> Convert(MemoryStream memoryStream)
        {
            var lines = GetStreamLines(memoryStream);

            return new List<FinancialTransaction>();
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